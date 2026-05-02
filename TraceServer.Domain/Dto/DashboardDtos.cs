using System;
using System.Collections.Generic;
using AgileAi.Domain.Models;

namespace AgileAi.Domain.Dto
{
    public class ActiveSprintSummaryDto
    {
        public Guid SprintId { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public int TotalStories { get; set; }
        public int DoneStories { get; set; }
        public int TotalIssues { get; set; }
        public int DoneIssues { get; set; }
        public int CompletedPoints { get; set; }
    }

    public class BoardColumnDto
    {
        public ItemStatus Status { get; set; }
        public IEnumerable<IssueResponseDto> Issues { get; set; }
    }

    public class TeamWorkloadDto
    {
        public Guid MemberId { get; set; }
        public string MemberName { get; set; }
        public int OpenIssueCount { get; set; }
    }

    public class ChartPointDto
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }

    public class BlockedOrOverdueIssueDto
    {
        public Guid IssueId { get; set; }
        public string Title { get; set; }
        public ItemStatus Status { get; set; }
        public Guid? AssigneeId { get; set; }
        public Guid UserStoryId { get; set; }
    }
}
