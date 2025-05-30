using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SkillSnap.Api.Models
{
    public class PortfolioUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }

        [InverseProperty("User")]
        public List<Project> Projects { get; set; }

        [InverseProperty("User")]
        public List<Skill> Skills { get; set; }
    }
}
