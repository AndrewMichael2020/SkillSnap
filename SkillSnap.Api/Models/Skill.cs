using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSnap.Api.Models
{
    public class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }

        public string PortfolioUserId { get; set; }

        [ForeignKey("PortfolioUserId")]
        public PortfolioUser User { get; set; }
    }
}
