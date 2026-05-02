using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class Issue
    {
        [Key]
        public Guid IssueId { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public ItemStatus Status { get; set; }
        public int Order { get; set; } // For Drag & Drop sequence [cite: 29]

        // Relations
        public Guid UserStoryId { get; set; }
        public UserStory UserStory { get; set; }

        public Guid? AssigneeId { get; set; } // Auto-assigned via IA [cite: 202]

        public ICollection<SubTask> SubTasks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Attachment> Attachments { get; set; }

        public bool isDeleted { get; set; } = false;

    }
}
