using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Models
{
    public class AIPredictionLog
    {
        [Key]
        public Guid PredictionId { get; set; } = Guid.NewGuid();
        public string PredictionType { get; set; } // Priority, Assignment, Velocity
        public string InputData { get; set; } // JSON of what was sent to ML
        public string PredictedValue { get; set; }
        public double ConfidenceScore { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool isDeleted { get; set; } = false;

    }
}
