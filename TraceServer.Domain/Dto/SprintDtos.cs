using System;
using System.ComponentModel.DataAnnotations;
using AgileAi.Domain.Models;

namespace AgileAi.Domain.Dto
{
    public class CreateSprintDto
    {
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }

    public class UpdateSprintDto
    {
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public ItemStatus Status { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Range(0, int.MaxValue)]
        public int CompletedPoints { get; set; }
    }

    public class SprintResponseDto
    {
        public Guid SprintId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ItemStatus Status { get; set; }
        public Guid ProjectId { get; set; }
        public int CompletedPoints { get; set; }
    }
}
