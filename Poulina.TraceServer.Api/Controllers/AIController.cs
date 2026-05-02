using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgileAi.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AgileAi.Data.Context;
using AgileAi.Domain.Dto;
using AgileAi.Domain.Models;

namespace AgileAi.Api.Controllers
{
    [Authorize(Policy = "CanUseAi")]
    [Route("api/ai")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IProjectAuthorizationService _projectAuthorization;

        public AIController(AppDbContext context, IProjectAuthorizationService projectAuthorization)
        {
            _context = context;
            _projectAuthorization = projectAuthorization;
        }

        [HttpPost("generate-description")]
        public async Task<IActionResult> GenerateDescription([FromBody] GenerateDescriptionRequestDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Title))
                return BadRequest(new ApiErrorResponse { Message = "Title is required.", Code = "TITLE_REQUIRED" });

            if (request.ProjectId.HasValue && !await _projectAuthorization.CanAccessProject(request.ProjectId.Value))
                return Forbid();

            var text = $"As a user, I want {request.Title.Trim()} so that the team can deliver measurable business value.";
            return Ok(await LogPrediction("Description", request, text, 0.82));
        }

        [HttpPost("generate-acceptance-criteria")]
        public async Task<IActionResult> GenerateAcceptanceCriteria([FromBody] GenerateDescriptionRequestDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Title))
                return BadRequest(new ApiErrorResponse { Message = "Title is required.", Code = "TITLE_REQUIRED" });

            var suggestions = new[]
            {
                $"Given valid input, when the user completes {request.Title}, then the system saves the result.",
                "Given invalid input, when the user submits the form, then clear validation messages are shown.",
                "Given the action succeeds, when the workflow completes, then related team members are notified."
            };

            return Ok(await LogPrediction("AcceptanceCriteria", request, string.Join(" | ", suggestions), 0.78, suggestions));
        }

        [HttpPost("generate-subtasks")]
        public async Task<IActionResult> GenerateSubTasks([FromBody] GenerateSubTasksRequestDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Title))
                return BadRequest(new ApiErrorResponse { Message = "Title is required.", Code = "TITLE_REQUIRED" });

            var suggestions = new[]
            {
                "Clarify requirements and edge cases",
                "Implement backend endpoint and validation",
                "Connect frontend workflow",
                "Add tests and update documentation"
            };

            return Ok(await LogPrediction("SubTasks", request, string.Join(" | ", suggestions), 0.75, suggestions));
        }

        [HttpPost("predict-priority/{userStoryId}")]
        public async Task<IActionResult> PredictPriority(Guid userStoryId)
        {
            if (!await _projectAuthorization.CanAccessUserStory(userStoryId))
                return Forbid();

            var story = await _context.UserStories.FirstOrDefaultAsync(us => us.UserStoryId == userStoryId);
            if (story == null)
                return NotFound();

            var priority = story.StoryPoints >= 8 ? "High" : story.StoryPoints >= 5 ? "Medium" : "Low";
            return Ok(await LogPrediction("Priority", new { userStoryId, story.StoryPoints }, priority, 0.7));
        }

        [HttpGet("sprint-risk/{sprintId}")]
        public async Task<IActionResult> PredictSprintRisk(Guid sprintId)
        {
            if (!await _projectAuthorization.CanAccessSprint(sprintId))
                return Forbid();

            var total = await _context.Issues.CountAsync(i => i.UserStory.SprintId == sprintId);
            var done = await _context.Issues.CountAsync(i => i.UserStory.SprintId == sprintId && (i.Status == ItemStatus.Done || i.Status == ItemStatus.Closed));
            var risk = total == 0 ? "Low" : done * 100 / total < 40 ? "High" : "Medium";

            return Ok(await LogPrediction("SprintRisk", new { sprintId, total, done }, risk, 0.72));
        }

        [HttpGet("daily-standup/{projectId}")]
        public async Task<IActionResult> GenerateDailyStandup(Guid projectId)
        {
            if (!await _projectAuthorization.CanAccessProject(projectId))
                return Forbid();

            var open = await _context.Issues.CountAsync(i => i.UserStory.Epic.ProjectId == projectId && i.Status != ItemStatus.Done && i.Status != ItemStatus.Closed);
            var review = await _context.Issues.CountAsync(i => i.UserStory.Epic.ProjectId == projectId && i.Status == ItemStatus.Review);
            var summary = $"Open tasks: {open}. In review: {review}. Focus on unblocking review items and finishing in-progress work.";

            return Ok(await LogPrediction("DailyStandup", new { projectId }, summary, 0.8));
        }

        [HttpGet("release-notes/{projectId}")]
        public async Task<IActionResult> GenerateReleaseNotes(Guid projectId)
        {
            if (!await _projectAuthorization.CanAccessProject(projectId))
                return Forbid();

            var completed = await _context.UserStories
                .Where(us => us.Epic.ProjectId == projectId && us.Status == SprintStatus.Done)
                .Select(us => us.Title)
                .Take(10)
                .ToListAsync();

            var notes = completed.Any()
                ? "Completed: " + string.Join("; ", completed)
                : "No completed stories are ready for release notes yet.";

            return Ok(await LogPrediction("ReleaseNotes", new { projectId }, notes, 0.76, completed));
        }

        private async Task<AiPredictionResponseDto> LogPrediction(string type, object input, string value, double confidence, IEnumerable<string> suggestions = null)
        {
            _context.AIPredictionLogs.Add(new AIPredictionLog
            {
                PredictionId = Guid.NewGuid(),
                PredictionType = type,
                InputData = JsonSerializer.Serialize(input),
                PredictedValue = value,
                ConfidenceScore = confidence,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return new AiPredictionResponseDto
            {
                PredictionType = type,
                SuggestedValue = value,
                ConfidenceScore = confidence,
                Suggestions = suggestions ?? Array.Empty<string>()
            };
        }
    }
}
