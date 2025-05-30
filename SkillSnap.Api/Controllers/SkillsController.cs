using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSnap.Api.Data;
using SkillSnap.Api.Models;
using SkillSnap.Shared;
using AutoMapper;

namespace SkillSnap.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SkillsController : ControllerBase
    {
        private readonly SkillSnapContext _context;
        private readonly IMapper _mapper;

        public SkillsController(SkillSnapContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills()
        {
            var skills = await _context.Skills.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<SkillDto>>(skills));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillDto>> GetSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            return Ok(_mapper.Map<SkillDto>(skill));
        }

        [HttpPost]
        public async Task<ActionResult<SkillDto>> CreateSkill(SkillDto dto)
        {
            var skill = _mapper.Map<Skill>(dto);
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, _mapper.Map<SkillDto>(skill));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, SkillDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            _mapper.Map(dto, skill);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
