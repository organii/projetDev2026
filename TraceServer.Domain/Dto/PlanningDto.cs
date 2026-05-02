using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Dto
{
    public class PlanningDto
    {
        public class EpicDto
        {
            public Guid EpicId { get; set; }
            public string Title { get; set; }
            public Guid ProjectId { get; set; }
        }

        public class SprintDto
        {
            public Guid SprintId { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Status { get; set; } // "Planned", "Active", "Closed"
            public Guid ProjectId { get; set; }
        }

        public class UserStoryDto
        {
            public Guid UserStoryId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int StoryPoints { get; set; }
            public string Priority { get; set; } // Low, Medium, High
            public Guid EpicId { get; set; }
            public Guid? SprintId { get; set; } // Nullable if in Backlog
        }
    }
}
