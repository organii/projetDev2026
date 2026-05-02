using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; } = Guid.NewGuid();
        public string Message { get; set; }
        public string Link { get; set; } // URL to the specific Issue/Project
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relations
        public Guid? ReceiverId { get; set; }
        public User? Receiver { get; set; }
    }
}
