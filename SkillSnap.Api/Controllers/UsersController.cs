using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillSnap.Api.Models;
using SkillSnap.Shared;
using AutoMapper;

namespace SkillSnap.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<PortfolioUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<PortfolioUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioUserDto>>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<PortfolioUserDto>>(users));
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioUserDto>> GetUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();
            return Ok(_mapper.Map<PortfolioUserDto>(user));
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<PortfolioUserDto>> CreateUser(PortfolioUserDto dto)
        {
            // For demo: create user with default password and User role
            var user = new PortfolioUser
            {
                UserName = dto.Name,
                Email = $"{dto.Name?.Replace(" ", "").ToLower()}@skillsnap.com",
                FullName = dto.Name,
                Bio = dto.Bio,
                ProfileImageUrl = dto.ProfileImageUrl,
                Projects = new List<Project>(),
                Skills = new List<Skill>()
            };
            var result = await _userManager.CreateAsync(user, "User123!");
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(user, "User");
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, _mapper.Map<PortfolioUserDto>(user));
        }
    }
}
