using System;

namespace AgileAi.Api.Dtos
{
    public class ProjectResponseDto
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string Key { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsFinished { get; set; }
        public DateTime? FinishedAt { get; set; }
        public int TotalCompletedPoints { get; set; }
    }
}
