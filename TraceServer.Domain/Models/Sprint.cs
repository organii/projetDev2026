using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class Sprint
    {
        [Key]
        public Guid SprintId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ItemStatus Status { get; set; } 

        // Relations
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public int CompletedPoints { get; set; } = 0;
        public ICollection<UserStory> UserStories { get; set; }
        public bool isDeleted { get; set; } = false;

    }
    public enum ItemStatus 
    { 
        Todo = 1,
        InProgress = 2,
        Review = 3,
        Done = 4 ,
        Closed = 5,
    }
}
