using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class UserStory
    {
        [Key]
        public Guid UserStoryId { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; } // Generated via NLP [cite: 206]
        public int StoryPoints { get; set; }
        public ItemPriority Priority { get; set; } // Predicted via ML.NET [cite: 205]
        public MoSCoW MoSCoW { get; set; }

        // Relations
        public Guid EpicId { get; set; }
        public Epic Epic { get; set; }

        public Guid? SprintId { get; set; } // Nullable if in Product Backlog
        public Sprint? Sprint { get; set; }

        public SprintStatus Status { get; set; } // Added field

        public ICollection<Issue> Issues { get; set; }
        public bool isDeleted { get; set; } = false;

    }
    public enum ItemPriority 
    { 
        Low=1,
        Medium=2,
        High=3,
        Critical=4
    }
    public enum MoSCoW
    {
        Must = 1,
        Should = 2,
        Could = 3,
        Wont = 4 
    }
    public enum SprintStatus
    {
        Todo = 1,
        InProgress = 2,
        InReview = 3,
        Done = 4
    }
}
