using System;
using System.Threading.Tasks;

namespace AgileAi.Api.Services
{
    public interface IProjectAuthorizationService
    {
        Task<bool> CanAccessProject(Guid projectId);
        Task<bool> CanAccessSprint(Guid sprintId);
        Task<bool> CanAccessUserStory(Guid userStoryId);
        Task<bool> CanAccessIssue(Guid issueId);
        Task<bool> CanAccessSubTask(Guid subTaskId);
        Task<bool> CanManageProject(Guid projectId);
        Task<bool> IsProjectMember(Guid projectId);
        Task<bool> IsProjectOwner(Guid projectId);
    }
}
