using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgileAi.Api.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgileAi.Data.Context;
using AgileAi.Domain.Dto;
using AgileAi.Domain.Models;

namespace AgileAi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IProjectAuthorizationService _projectAuthorization;

        public DashboardController(
            AppDbContext context,
            ICurrentUserService currentUser,
            IProjectAuthorizationService projectAuthorization)
        {
            _context = context;
            _currentUser = currentUser;
            _projectAuthorization = projectAuthorization;
        }

        [HttpGet("my-projects")]
        public async Task<IActionResult> GetMyProjects()
        {
            var userId = _currentUser.UserId;

            var projects = await _context.Projects
                .Where(p => _currentUser.IsAdmin || p.OwnerId == userId || p.Members.Any(m => m.MemberId == userId))
                .Select(p => new ProjectResponseDto
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.ProjectName,
                    ProjectDescription = p.ProjectDescription,
                    Key = p.Key,
                    OwnerId = p.OwnerId,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    IsFinished = p.IsFinished,
                    FinishedAt = p.FinishedAt,
                    TotalCompletedPoints = p.TotalCompletedPoints
                })
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet("active-sprint/{projectId}")]
        public async Task<IActionResult> GetActiveSprintSummary(Guid projectId)
        {
            if (!await _projectAuthorization.CanAccessProject(projectId))
                return Forbid();

            var sprint = await _context.Sprints
                .Where(s => s.ProjectId == projectId && s.Status == ItemStatus.InProgress)
                .OrderByDescending(s => s.StartDate)
                .FirstOrDefaultAsync();

            if (sprint == null)
                return NotFound();

            var stories = _context.UserStories.Where(us => us.SprintId == sprint.SprintId);
            var issues = _context.Issues.Where(i => i.UserStory.SprintId == sprint.SprintId);

            return Ok(new ActiveSprintSummaryDto
            {
                SprintId = sprint.SprintId,
                Name = sprint.Name,
                ProjectId = sprint.ProjectId,
                TotalStories = await stories.CountAsync(),
                DoneStories = await stories.CountAsync(us => us.Status == SprintStatus.Done),
                TotalIssues = await issues.CountAsync(),
                DoneIssues = await issues.CountAsync(i => i.Status == ItemStatus.Done || i.Status == ItemStatus.Closed),
                CompletedPoints = sprint.CompletedPoints
            });
        }

        [HttpGet("sprint-board/{sprintId}")]
        public async Task<IActionResult> GetSprintBoard(Guid sprintId)
        {
            if (!await _projectAuthorization.CanAccessSprint(sprintId))
                return Forbid();

            var issues = await _context.Issues
                .Where(i => i.UserStory.SprintId == sprintId)
                .Select(i => new IssueResponseDto
                {
                    IssueId = i.IssueId,
                    Title = i.Title,
                    Status = i.Status,
                    Order = i.Order,
                    UserStoryId = i.UserStoryId,
                    AssigneeId = i.AssigneeId
                })
                .ToListAsync();

            var board = issues
                .GroupBy(i => i.Status)
                .Select(group => new BoardColumnDto
                {
                    Status = group.Key,
                    Issues = group.OrderBy(i => i.Order).ToList()
                });

            return Ok(board);
        }

        [HttpGet("team-workload/{projectId}")]
        public async Task<IActionResult> GetTeamWorkload(Guid projectId)
        {
            if (!await _projectAuthorization.CanAccessProject(projectId))
                return Forbid();

            var members = await _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .Select(pm => new TeamWorkloadDto
                {
                    MemberId = pm.MemberId,
                    MemberName = pm.Member.Nom + " " + pm.Member.Prenom,
                    OpenIssueCount = _context.Issues.Count(i =>
                        i.AssigneeId == pm.MemberId &&
                        i.UserStory.Epic.ProjectId == projectId &&
                        i.Status != ItemStatus.Done &&
                        i.Status != ItemStatus.Closed)
                })
                .ToListAsync();

            return Ok(members);
        }

        [HttpGet("burndown/{sprintId}")]
        public async Task<IActionResult> GetBurndown(Guid sprintId)
        {
            if (!await _projectAuthorization.CanAccessSprint(sprintId))
                return Forbid();

            var totalPoints = await _context.UserStories
                .Where(us => us.SprintId == sprintId)
                .SumAsync(us => us.StoryPoints);

            var donePoints = await _context.UserStories
                .Where(us => us.SprintId == sprintId && us.Status == SprintStatus.Done)
                .SumAsync(us => us.StoryPoints);

            return Ok(new[]
            {
                new ChartPointDto { Label = "Remaining", Value = totalPoints - donePoints },
                new ChartPointDto { Label = "Done", Value = donePoints }
            });
        }

        [HttpGet("velocity/{projectId}")]
        public async Task<IActionResult> GetVelocity(Guid projectId)
        {
            if (!await _projectAuthorization.CanAccessProject(projectId))
                return Forbid();

            var points = await _context.Sprints
                .Where(s => s.ProjectId == projectId && s.Status == ItemStatus.Closed)
                .OrderBy(s => s.EndDate)
                .Select(s => new ChartPointDto
                {
                    Label = s.Name,
                    Value = s.CompletedPoints
                })
                .ToListAsync();

            return Ok(points);
        }

        [HttpGet("blocked-overdue/{projectId}")]
        public async Task<IActionResult> GetBlockedOrOverdueIssues(Guid projectId)
        {
            if (!await _projectAuthorization.CanAccessProject(projectId))
                return Forbid();

            var issues = await _context.Issues
                .Where(i => i.UserStory.Epic.ProjectId == projectId && i.Status == ItemStatus.Review)
                .Select(i => new BlockedOrOverdueIssueDto
                {
                    IssueId = i.IssueId,
                    Title = i.Title,
                    Status = i.Status,
                    AssigneeId = i.AssigneeId,
                    UserStoryId = i.UserStoryId
                })
                .ToListAsync();

            return Ok(issues);
        }
    }
}
