using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgileAi.Api.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgileAi.Data.Context;
using AgileAi.Domain.Commands;
using AgileAi.Domain.Dto;
using AgileAi.Domain.Models;
using AgileAi.Domain.Queries;

namespace AgileAi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectMembersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProjectAuthorizationService _projectAuthorization;
        private readonly AppDbContext _context;

        public ProjectMembersController(
            IMediator mediator,
            IProjectAuthorizationService projectAuthorization,
            AppDbContext context)
        {
            _mediator = mediator;
            _projectAuthorization = projectAuthorization;
            _context = context;
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetProjectStaff(Guid projectId)
        {
            if (!await _projectAuthorization.CanAccessProject(projectId))
                return Forbid();

            var query = new GetListGenericQuery<ProjectMember>(pm => pm.ProjectId == projectId);
            var result = await _mediator.Send(query);
            return Ok(result.Select(ToResponse));
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff([FromBody] CreateProjectMemberDto request)
        {
            if (request == null)
                return BadRequest();

            if (!await _projectAuthorization.CanManageProject(request.ProjectId))
                return Forbid();

            var result = await _mediator.Send(new AddProjectMemberCommand(request.ProjectId, request.MemberId));
            return Ok(ToResponse(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStaff(Guid id)
        {
            var member = await _context.ProjectMembers.FirstOrDefaultAsync(pm => pm.ProjectMemberId == id);

            if (member == null)
                return NotFound();

            if (!await _projectAuthorization.CanManageProject(member.ProjectId))
                return Forbid();

            var result = await _mediator.Send(new RemoveProjectMemberCommand(id));
            return result != null ? Ok(ToResponse(result)) : NotFound();
        }

        private static ProjectMemberResponseDto ToResponse(ProjectMember member)
        {
            return new ProjectMemberResponseDto
            {
                ProjectMemberId = member.ProjectMemberId,
                ProjectId = member.ProjectId,
                MemberId = member.MemberId
            };
        }
    }
}
