using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgileAi.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SprintsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProjectAuthorizationService _projectAuthorization;

        public SprintsController(IMediator mediator, IProjectAuthorizationService projectAuthorization)
        {
            _mediator = mediator;
            _projectAuthorization = projectAuthorization;
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetSprintsByProject(Guid projectId)
        {
            if (!await _projectAuthorization.CanAccessProject(projectId))
                return Forbid();

            var query = new GetListGenericQuery<Sprint>(s => s.ProjectId == projectId);
            var result = await _mediator.Send(query);
            return Ok(result.Select(ToResponse));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSprint([FromBody] CreateSprintDto request)
        {
            if (request == null)
                return BadRequest();

            if (!await _projectAuthorization.CanManageProject(request.ProjectId))
                return Forbid();

            if (request.EndDate <= request.StartDate)
                return BadRequest(new ApiErrorResponse
                {
                    Message = "Sprint end date must be after start date.",
                    Code = "INVALID_SPRINT_DATES"
                });

            var result = await _mediator.Send(new CreateSprintCommand(
                request.Name,
                request.StartDate,
                request.EndDate,
                request.ProjectId));

            return Ok(ToResponse(result));
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> StartSprint(Guid id)
        {
            if (!await _projectAuthorization.CanAccessSprint(id))
                return Forbid();

            var result = await _mediator.Send(new StartSprintCommand(id));
            return result != null ? Ok(ToResponse(result)) : NotFound();
        }

        [HttpPost("{id}/close")]
        public async Task<IActionResult> CloseSprint(Guid id)
        {
            if (!await _projectAuthorization.CanAccessSprint(id))
                return Forbid();

            var result = await _mediator.Send(new CloseSprintCommand(id));
            return result != null ? Ok(ToResponse(result)) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSprint(Guid id, [FromBody] UpdateSprintDto request)
        {
            if (request == null)
                return BadRequest();

            if (!await _projectAuthorization.CanManageProject(request.ProjectId))
                return Forbid();

            if (request.EndDate <= request.StartDate)
                return BadRequest(new ApiErrorResponse
                {
                    Message = "Sprint end date must be after start date.",
                    Code = "INVALID_SPRINT_DATES"
                });

            if (!Enum.IsDefined(typeof(ItemStatus), request.Status))
                return BadRequest(new ApiErrorResponse
                {
                    Message = "Invalid sprint status.",
                    Code = "INVALID_SPRINT_STATUS"
                });

            var sprint = new Sprint
            {
                SprintId = id,
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ProjectId = request.ProjectId,
                Status = request.Status,
                CompletedPoints = request.CompletedPoints
            };

            var result = await _mediator.Send(new PutGenericCommand<Sprint>(id, sprint));
            return Ok(ToResponse(result));
        }

        private static SprintResponseDto ToResponse(Sprint sprint)
        {
            return new SprintResponseDto
            {
                SprintId = sprint.SprintId,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                Status = sprint.Status,
                ProjectId = sprint.ProjectId,
                CompletedPoints = sprint.CompletedPoints
            };
        }
    }
}
