using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class AcceptanceCriterion
    {
        [Key]
        public Guid CriterionId { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public bool IsSatisfied { get; set; } = false;

        // Relations
        public Guid UserStoryId { get; set; }
        public UserStory UserStory { get; set; }
        public bool isDeleted { get; set; } = false;


    }
}
