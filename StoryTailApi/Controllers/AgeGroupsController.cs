using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoryTailBlazor.Data;
using StoryTailBlazor.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryTailApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class AgeGroupsController : ControllerBase
    {
        private readonly StoryTailDbContext _context;

        public AgeGroupsController(StoryTailDbContext context)
        {
            _context = context;
        }

        // GET: api/agegroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgeGroupDto>>> GetAgeGroups()
        {
            var ageGroups = await _context.AgeGroups
                .Select(ag => new AgeGroupDto
                {
                    Id = ag.Id,
                    AgeGroupDescription = ag.AgeGroupDescription
                })
                .ToListAsync();

            if (ageGroups == null || ageGroups.Count == 0)
            {
                return NotFound("No age groups found.");
            }

            return Ok(ageGroups);  // Retorna os grupos etários em formato de DTO
        }

        // GET: api/agegroups/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AgeGroupDto>> GetAgeGroup(int id)
        {
            var ageGroup = await _context.AgeGroups
                .Where(ag => ag.Id == id)
                .Select(ag => new AgeGroupDto
                {
                    Id = ag.Id,
                    AgeGroupDescription = ag.AgeGroupDescription
                })
                .FirstOrDefaultAsync();

            if (ageGroup == null)
            {
                return NotFound($"Age group with ID {id} not found.");
            }

            return Ok(ageGroup);  // Retorna o grupo etário em formato de DTO
        }
    }

    // DTO class for AgeGroup
    public class AgeGroupDto
    {
        public int Id { get; set; }
        public string AgeGroupDescription { get; set; }
    }
}
