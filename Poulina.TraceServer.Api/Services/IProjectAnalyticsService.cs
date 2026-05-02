using System;
using System.Threading.Tasks;

namespace AgileAi.Api.Services
{
    public interface IProjectAnalyticsService
    {
        Task<int> GetCompletedPointsForSprint(Guid sprintId);
        Task<int> CalculateCompletedPoints(Guid sprintId);
        Task<int> GetTotalCompletedPointsForProject(Guid projectId);
        Task FinalizeProject(Guid projectId);
        Task CloseSprint(Guid sprintId);
    }
}