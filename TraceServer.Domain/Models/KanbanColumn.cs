using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class KanbanColumn
    {
        [Key]
        public Guid ColumnId { get; set; } = Guid.NewGuid();
        public ItemStatus Status { get; set; } // Todo, InProgress, etc.
        public int WipLimit { get; set; } // Max tasks allowed in this column

        // Relations
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public bool isDeleted { get; set; } = false;
    }
}
