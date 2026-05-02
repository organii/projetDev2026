using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Dto
{
    public class ExecutionDto
    {
        public class IssueDto
        {
            public Guid IssueId { get; set; }
            public string Title { get; set; }
            public string Status { get; set; } // "Todo", "InProgress", "Done"
            public Guid? AssigneeId { get; set; }
            public Guid UserStoryId { get; set; }
        }

        public class CommentDto
        {
            public Guid CommentId { get; set; }
            public string Content { get; set; }
            public Guid AuthorId { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public class PredictionDto
        {
            public string SuggestedValue { get; set; }
            public float ConfidenceScore { get; set; }
        }
    }
}
