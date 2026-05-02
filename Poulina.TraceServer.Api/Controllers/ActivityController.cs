using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgileAi.Api.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgileAi.Data.Context;
using AgileAi.Domain.Dto;

namespace AgileAi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IProjectAuthorizationService _projectAuthorization;

        public ActivityController(AppDbContext context, IProjectAuthorizationService projectAuthorization)
        {
            _context = context;
            _projectAuthorization = projectAuthorization;
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetProjectActivity(Guid projectId)
        {
            if (!await _projectAuthorization.CanAccessProject(projectId))
                return Forbid();

            var activity = await _context.ActivityLogs
                .Where(a => a.ProjectId == projectId)
                .OrderByDescending(a => a.CreatedAt)
                .Take(100)
                .Select(a => new ActivityLogResponseDto
                {
                    ActivityLogId = a.ActivityLogId,
                    ProjectId = a.ProjectId,
                    ActorId = a.ActorId,
                    Action = a.Action,
                    EntityType = a.EntityType,
                    EntityId = a.EntityId,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            return Ok(activity);
        }
    }
}
