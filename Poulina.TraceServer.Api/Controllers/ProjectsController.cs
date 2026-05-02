using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgileAi.Api.Services;
using System;
using System.Threading.Tasks;
using AgileAi.Domain.Commands;
using AgileAi.Domain.Dto;
using AgileAi.Domain.Models;
using AgileAi.Domain.Queries;

namespace AgileAi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProjectAuthorizationService _projectAuthorization;
        private readonly ICurrentUserService _currentUser;

        public ProjectsController(
            IMediator mediator,
            IProjectAuthorizationService projectAuthorization,
            ICurrentUserService currentUser)
        {
            _mediator = mediator;
            _projectAuthorization = projectAuthorization;
            _currentUser = currentUser;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!await _projectAuthorization.CanAccessProject(id))
                return Forbid();

            var query = new GetGenericQuery<Project>(p => p.ProjectId == id);
            var result = await _mediator.Send(query);

            return result != null ? Ok(ToResponse(result)) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto request)
        {
            if (request == null)
                return BadRequest();

            var result = await _mediator.Send(new CreateProjectCommand(
                request.ProjectName,
                request.ProjectDescription,
                request.Key,
                _currentUser.UserId));

            return Ok(ToResponse(result));
        }

        private static ProjectResponseDto ToResponse(Project project)
        {
            return new ProjectResponseDto
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription,
                Key = project.Key,
                OwnerId = project.OwnerId,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
                IsFinished = project.IsFinished,
                FinishedAt = project.FinishedAt,
                TotalCompletedPoints = project.TotalCompletedPoints
            };
        }
    }
}
