using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class ProjectMember
    {
        [Key]
        public Guid ProjectMemberId { get; set; } = Guid.NewGuid();
        public Guid ProjectId { get; set; } 
        public Project  Project { get; set; }
        public Guid MemberId { get; set; }
        public User Member { get; set; }
        public bool isDeleted { get; set; } = false;


    }
    public enum ProjectRole 
    { 
        Admin = 1,
        ProductOwner =2,
        ScrumMaster=3,
        Developer =4 
    }
}

