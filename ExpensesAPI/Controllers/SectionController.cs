using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesAPI.Controllers
{
    [Route("api/Section")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ILogger<SectionController> _logger;
        private readonly ApplicationDbContext _context;

        public SectionController(ILogger<SectionController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            if (_context.Sections == null)
            {
                _logger.LogError("NULL SECTIONS.");
                return NotFound("Null Sections.");
            }

            _logger.LogInformation("GETTING SECTIONS...");
            return Ok(_context.Sections);
        }


        
        [HttpGet("Get/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetId(int id)
        {
            if (id == 0)
            {
                _logger.LogError("ID CANNOT BE 0");
                return BadRequest("Id can't be 0.");
            }

            var category = _context.Sections.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                _logger.LogError("ID == NULL");
                return NotFound("Section is not found.");
            }

            _logger.LogInformation("Getting Section");
            return Ok(category);
        }



        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create(SectionDto section)
        {
            var category = new Section
            {
                Id = section.Id,
                Name = section.Name,
            };

            _context.Add(category);
            _context.SaveChanges();

            _logger.LogInformation("CREATING SECTIONS...");
            return Ok("Section successfully created!!!");
        }



        [HttpPut("Update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, SectionDto section)
        {
            if(id == 0)
            {
                _logger.LogError("ID CANNOT BE 0");
                return BadRequest("Id can't be 0.");
            }

            var category = _context.Sections.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                _logger.LogError("ID == NULL");
                return NotFound("Section is not found.");
            }

            category.Name = section.Name;

            _context.SaveChanges();

            _logger.LogInformation("Section is updated.");
            return Ok("Section is updated.");
        }



        [HttpDelete("Delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                _logger.LogError("ID CANNOT BE 0");
                return BadRequest("Id can't be 0.");
            }

            var category = _context.Sections.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                _logger.LogError("ID == NULL");
                return NotFound("Section is not found.");
            }

            _context.Remove(category);
            _context.SaveChanges();

            _logger.LogInformation("Section is deleted");
            return Ok("Section is deleted");
        }

    }
}
