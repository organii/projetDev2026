using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class Project
    {
        [Key]
        public Guid ProjectId { get; set; } = Guid.NewGuid();
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string Key { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsFinished { get; set; }
        public DateTime? FinishedAt { get; set; }
        public int TotalCompletedPoints { get; set; }

        public bool isDeleted { get; set; } = false;
        public ICollection<Sprint> Sprints { get; set; }
        public ICollection<Epic> Epics { get; set; }
        public ICollection<ProjectMember> Members { get; set; }

    }
}
