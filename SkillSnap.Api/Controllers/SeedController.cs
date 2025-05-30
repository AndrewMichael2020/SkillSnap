using Microsoft.AspNetCore.Mvc;
using SkillSnap.Api.Data;
using SkillSnap.Api.Models;

namespace SkillSnap.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly SkillSnapContext _context;

        public SeedController(SkillSnapContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Seed()
        {
            if (_context.PortfolioUsers.Any())
                return BadRequest("Database already seeded.");

            var user = new PortfolioUser
            {
                Name = "Chad McSynergy",
                Bio = "Serial entrepreneur, coffee enthusiast, and professional buzzword generator.",
                ProfileImageUrl = "https://example.com/images/chad.jpg",
                Projects = new List<Project>(),
                Skills = new List<Skill>()
            };

            var projectTitles = Enumerable.Range(1, 20).Select(i => $"Project Synergy {i}: The Pivotening").ToList();
            var projectDescriptions = Enumerable.Range(1, 20).Select(i =>
                $"A disruptive solution to optimize blockchain-driven, cloud-based, AI-powered business paradigms. (Phase {i})").ToList();

            for (int i = 0; i < 20; i++)
            {
                user.Projects.Add(new Project
                {
                    Title = projectTitles[i],
                    Description = projectDescriptions[i],
                    ImageUrl = $"https://example.com/projects/funny{i+1}.jpg",
                    User = user
                });
            }

            var skillNames = new[]
            {
                "Synergy Maximization",
                "Coffee-Driven Coding",
                "Blockchain Buzzwording",
                "Agile Standup Comedy",
                "Cloud-Native Napping",
                "Pivoting Under Pressure",
                "KPI Guesswork",
                "Scrum Mastery (of Ceremonies)",
                "PowerPoint Karaoke",
                "AI Hype Generation",
                "Remote Meeting Survival",
                "Business Casual Fashion",
                "Deadline Dodging",
                "Vision Statement Crafting",
                "Email Ninja"
            };

            var skillLevels = new[]
            {
                "Expert", "Guru", "Ninja", "Rockstar", "Wizard", "Evangelist", "Champion", "Overlord", "Sensei", "Maestro",
                "Virtuoso", "Connoisseur", "Maven", "Enthusiast", "Prodigy"
            };

            for (int i = 0; i < 15; i++)
            {
                user.Skills.Add(new Skill
                {
                    Name = skillNames[i],
                    Level = skillLevels[i],
                    User = user
                });
            }

            _context.PortfolioUsers.Add(user);
            _context.SaveChanges();

            return Ok("Database seeded with 1 user, 20 projects, and 15 skills.");
        }
    }
}
