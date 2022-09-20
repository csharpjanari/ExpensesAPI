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
    }
}
