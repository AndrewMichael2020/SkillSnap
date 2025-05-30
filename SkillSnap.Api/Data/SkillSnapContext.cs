using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillSnap.Api.Models;

namespace SkillSnap.Api.Data
{
    public class SkillSnapContext : IdentityDbContext<PortfolioUser>
    {
        public SkillSnapContext(DbContextOptions<SkillSnapContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
