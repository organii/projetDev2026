using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AgileAi.Domain.Commands;
using AgileAi.Domain.Interfaces;
using AgileAi.Domain.Models;

namespace AgileAi.Domain.Handlers
{
    public class AutoAssignHandler : IRequestHandler<AutoAssignCommand, Issue>
    {
        private readonly IGenericRepository<Issue> _issueRepository;
        private readonly IGenericRepository<ProjectMember> _projectMemberRepository;
        private readonly IGenericRepository<Project> _projectRepository;

        public AutoAssignHandler(
            IGenericRepository<Issue> issueRepository,
            IGenericRepository<ProjectMember> projectMemberRepository,
            IGenericRepository<Project> projectRepository)
        {
            _issueRepository = issueRepository;
            _projectMemberRepository = projectMemberRepository;
            _projectRepository = projectRepository;
        }

        public async Task<Issue> Handle(AutoAssignCommand request, CancellationToken cancellationToken)
        {
            var issue = _issueRepository.Get(
                i => i.IssueId == request.IssueId,
                query => query.Include(i => i.UserStory).ThenInclude(us => us.Epic));

            if (issue == null)
                return null;

            var projectId = issue.UserStory?.Epic?.ProjectId;
            if (!projectId.HasValue || projectId == Guid.Empty)
                return issue;

            var memberIds = _projectMemberRepository
                .GetList(pm => pm.ProjectId == projectId.Value)
                .Select(pm => pm.MemberId)
                .Distinct()
                .ToList();

            var ownerId = _projectRepository.Get(p => p.ProjectId == projectId.Value)?.OwnerId ?? Guid.Empty;

            if (ownerId != Guid.Empty && !memberIds.Contains(ownerId))
                memberIds.Add(ownerId);

            if (!memberIds.Any())
                return issue;

            var candidateWorkloads = _issueRepository
                .GetList(
                    i =>
                    i.AssigneeId.HasValue &&
                    memberIds.Contains(i.AssigneeId.Value) &&
                    i.UserStory.Epic.ProjectId == projectId.Value &&
                    i.Status != ItemStatus.Done &&
                    i.Status != ItemStatus.Closed,
                    query => query.Include(i => i.UserStory).ThenInclude(us => us.Epic))
                .GroupBy(i => i.AssigneeId.Value)
                .Select(g => new
                {
                    MemberId = g.Key,
                    Workload = g.Count()
                })
                .ToList();

            var bestMemberId = memberIds
                .Select(memberId => new
                {
                    MemberId = memberId,
                    Workload = candidateWorkloads.FirstOrDefault(x => x.MemberId == memberId)?.Workload ?? 0
                })
                .OrderBy(x => x.Workload)
                .ThenBy(x => x.MemberId)
                .Select(x => (Guid?)x.MemberId)
                .FirstOrDefault();

            if (!bestMemberId.HasValue)
                return issue;

            issue.AssigneeId = bestMemberId.Value;
            _issueRepository.Put(issue);
            await Task.CompletedTask;
            return issue;
        }
    }
}
