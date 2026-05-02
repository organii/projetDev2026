using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class ScrumCeremony
    {
        [Key]
        public Guid CeremonyId { get; set; } = Guid.NewGuid();
        public string Type { get; set; } // Daily, Review, Retro
        public string Notes { get; set; }
        public string Obstacles { get; set; } // "Blockers" mentioned in PPT
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Relations
        public Guid SprintId { get; set; }
        public Sprint Sprint { get; set; }
    }
}
