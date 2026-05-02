using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgileAi.Data.Context;

namespace AgileAi.Api.Services
{
    public class ProjectAuthorizationService : IProjectAuthorizationService
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public ProjectAuthorizationService(AppDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<bool> CanAccessProject(Guid projectId)
        {
            if (!_currentUser.IsAuthenticated)
                return false;

            if (_currentUser.IsAdmin)
                return true;

            return await IsProjectOwner(projectId) || await IsProjectMember(projectId);
        }

        public async Task<bool> CanAccessSprint(Guid sprintId)
        {
            var projectId = await _context.Sprints
                .Where(s => s.SprintId == sprintId)
                .Select(s => (Guid?)s.ProjectId)
                .FirstOrDefaultAsync();

            return projectId.HasValue && await CanAccessProject(projectId.Value);
        }

        public async Task<bool> CanAccessUserStory(Guid userStoryId)
        {
            var projectId = await _context.UserStories
                .Where(us => us.UserStoryId == userStoryId)
                .Select(us => (Guid?)us.Epic.ProjectId)
                .FirstOrDefaultAsync();

            return projectId.HasValue && await CanAccessProject(projectId.Value);
        }

        public async Task<bool> CanAccessIssue(Guid issueId)
        {
            var projectId = await _context.Issues
                .Where(i => i.IssueId == issueId)
                .Select(i => (Guid?)i.UserStory.Epic.ProjectId)
                .FirstOrDefaultAsync();

            return projectId.HasValue && await CanAccessProject(projectId.Value);
        }

        public async Task<bool> CanAccessSubTask(Guid subTaskId)
        {
            var projectId = await _context.SubTasks
                .Where(st => st.SubTaskId == subTaskId && st.IssueId.HasValue)
                .Select(st => (Guid?)st.Issue.UserStory.Epic.ProjectId)
                .FirstOrDefaultAsync();

            return projectId.HasValue && await CanAccessProject(projectId.Value);
        }

        public async Task<bool> CanManageProject(Guid projectId)
        {
            if (!_currentUser.IsAuthenticated)
                return false;

            if (_currentUser.IsAdmin)
                return true;

            return await IsProjectOwner(projectId);
        }

        public async Task<bool> IsProjectMember(Guid projectId)
        {
            if (_currentUser.UserId == Guid.Empty)
                return false;

            return await _context.ProjectMembers.AnyAsync(pm =>
                pm.ProjectId == projectId &&
                pm.MemberId == _currentUser.UserId);
        }

        public async Task<bool> IsProjectOwner(Guid projectId)
        {
            if (_currentUser.UserId == Guid.Empty)
                return false;

            return await _context.Projects.AnyAsync(p =>
                p.ProjectId == projectId &&
                p.OwnerId == _currentUser.UserId);
        }
    }
}
