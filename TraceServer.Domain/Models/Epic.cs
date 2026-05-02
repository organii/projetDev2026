using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class Epic
    {
        [Key]
        public Guid EpicId { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }

        // Relations
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<UserStory> UserStories { get; set; }
        public bool isDeleted { get; set; } = false;

    }
}
