using System;
using System.ComponentModel.DataAnnotations;

namespace AgileAi.Domain.Models
{
    public class ActivityLog
    {
        [Key]
        public Guid ActivityLogId { get; set; } = Guid.NewGuid();
        public Guid ProjectId { get; set; }
        public Guid? ActorId { get; set; }
        public string Action { get; set; }
        public string EntityType { get; set; }
        public Guid? EntityId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool isDeleted { get; set; } = false;
    }
}
