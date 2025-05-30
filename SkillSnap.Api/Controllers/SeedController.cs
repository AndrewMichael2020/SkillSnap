using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSnap.Api.Data;
using SkillSnap.Api.Models;

namespace SkillSnap.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly SkillSnapContext _context;
        private readonly UserManager<PortfolioUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedController(
            SkillSnapContext context,
            UserManager<PortfolioUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Seed()
        {
            // Seed roles
            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed default admin user
            var adminEmail = "admin@skillsnap.com";
            var adminPassword = "Admin123!";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new PortfolioUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Default Admin",
                    Bio = "Platform administrator.",
                    ProfileImageUrl = "https://example.com/images/admin.jpg",
                    Projects = new List<Project>(),
                    Skills = new List<Skill>()
                };
                var result = await _userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    return BadRequest($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                // Ensure admin is in Admin role
                if (!await _userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed a single sample user and one project/skill for test
            if (await _userManager.Users.AnyAsync(u => u.Email != adminEmail))
                return BadRequest("Database already seeded with sample user.");

            var user = new PortfolioUser
            {
                UserName = "chad@synergy.com",
                Email = "chad@synergy.com",
                FullName = "Chad McSynergy",
                Bio = "Serial entrepreneur, coffee enthusiast, and professional buzzword generator.",
                ProfileImageUrl = "https://example.com/images/chad.jpg",
                Projects = new List<Project>(),
                Skills = new List<Skill>()
            };

            // Only one project, do NOT set Id
            user.Projects.Add(new Project
            {
                Title = "Project Synergy 1: The Pivotening",
                Description = "A disruptive solution to optimize blockchain-driven, cloud-based, AI-powered business paradigms. (Phase 1)",
                ImageUrl = "https://example.com/projects/funny1.jpg",
                // Do NOT set Id, let EF/SQLite handle it
            });

            // Only one skill, do NOT set Id
            user.Skills.Add(new Skill
            {
                Name = "Synergy Maximization",
                Level = "Expert",
                // Do NOT set Id, let EF/SQLite handle it
            });

            // Save user via UserManager
            var userResult = await _userManager.CreateAsync(user, "Chad123!");
            if (!userResult.Succeeded)
                return BadRequest($"Failed to create sample user: {string.Join(", ", userResult.Errors.Select(e => e.Description))}");
            await _userManager.AddToRoleAsync(user, "User");

            // Add project and skill to DB
            foreach (var project in user.Projects)
            {
                project.PortfolioUserId = user.Id;
                _context.Projects.Add(project);
            }
            foreach (var skill in user.Skills)
            {
                skill.PortfolioUserId = user.Id;
                _context.Skills.Add(skill);
            }
            await _context.SaveChangesAsync();

            return Ok("Database seeded with admin, 1 user, 1 project, and 1 skill.");
        }
    }
}
