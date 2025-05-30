using System.ComponentModel.DataAnnotations;

namespace SkillSnap.Shared
{
    public record ProjectDto
    {
        public int Id { get; init; }

        [Required]
        [StringLength(100)]
        public string Title { get; init; }

        [StringLength(1000)]
        public string Description { get; init; }

        [StringLength(200)]
        public string ImageUrl { get; init; }

        [Required]
        public string PortfolioUserId { get; init; }
    }
}
