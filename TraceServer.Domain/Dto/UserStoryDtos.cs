using System;
using System.ComponentModel.DataAnnotations;
using AgileAi.Domain.Models;

namespace AgileAi.Domain.Dto
{
    public class CreateUserStoryDto
    {
        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Range(1, 100)]
        public int StoryPoints { get; set; }

        [Required]
        public ItemPriority Priority { get; set; }

        [Required]
        public MoSCoW MoSCoW { get; set; }

        [Required]
        public Guid EpicId { get; set; }

        public Guid? SprintId { get; set; }

        [Required]
        public SprintStatus Status { get; set; }
    }

    public class UpdateUserStoryDto
    {
        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Range(1, 100)]
        public int StoryPoints { get; set; }

        [Required]
        public ItemPriority Priority { get; set; }

        [Required]
        public MoSCoW MoSCoW { get; set; }

        [Required]
        public Guid EpicId { get; set; }

        public Guid? SprintId { get; set; }

        [Required]
        public SprintStatus Status { get; set; }
    }

    public class UserStoryResponseDto
    {
        public Guid UserStoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StoryPoints { get; set; }
        public ItemPriority Priority { get; set; }
        public MoSCoW MoSCoW { get; set; }
        public Guid EpicId { get; set; }
        public Guid? SprintId { get; set; }
        public SprintStatus Status { get; set; }
    }
}
