using System;
using System.Linq;
using System.Threading.Tasks;
using AgileAi.Domain.Interfaces;
using AgileAi.Domain.Models;

namespace AgileAi.Api.Services
{
    public class ProjectAnalyticsService : IProjectAnalyticsService
    {
        private readonly IGenericRepository<UserStory> _storyRepository;
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IGenericRepository<Sprint> _sprintRepo;

        public ProjectAnalyticsService(
            IGenericRepository<UserStory> storyRepository,
            IGenericRepository<Project> projectRepository,
            IGenericRepository<Sprint> sprintRepository)
        {
            _storyRepository = storyRepository;
            _projectRepository = projectRepository;
            _sprintRepo = sprintRepository;
        }

        public async Task CloseSprint(Guid sprintId)
        {
            // 1. Fetch the sprint
            var sprint = _sprintRepo.Get(s => s.SprintId == sprintId);
            if (sprint == null) return;

            // 2. Calculate points for stories marked as 'Done' in this sprint
            var doneStories = _storyRepository.GetList(us =>
                us.SprintId == sprintId &&
                us.Status == (SprintStatus)3
            );

            // 3. Set the attribute and close the sprint
            sprint.CompletedPoints = doneStories.Sum(us => us.StoryPoints);
            sprint.Status = ItemStatus.Closed;

            // 4. Update via generic repository
            _sprintRepo.Put(sprint);
        }

        public async Task<int> CalculateCompletedPoints(Guid projectId)
        {
            // 1. Get all stories for this project that are 'Done'
            // We use your Generic Repository's GetAll with a filter
            var completedStories = _storyRepository.GetList(
                us => us.Epic.ProjectId == projectId && us.Status == (SprintStatus)3
            );

            // 2. Sum the StoryPoints
            return completedStories.Sum(us => us.StoryPoints);
        }

        public async Task FinalizeProject(Guid projectId)
        {
            var project = _projectRepository.Get(p => p.ProjectId == projectId);
            if (project == null) return;

            // Calculate final tally
            project.TotalCompletedPoints = await CalculateCompletedPoints(projectId);
            project.IsFinished = true;
            project.FinishedAt = DateTime.UtcNow;

            // Update using your generic logic
            _projectRepository.Put(project);
        }

        public Task<int> GetCompletedPointsForSprint(Guid sprintId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalCompletedPointsForProject(Guid projectId)
        {
            throw new NotImplementedException();
        }
    }
}
