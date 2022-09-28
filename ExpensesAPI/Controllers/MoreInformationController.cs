using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesAPI.Controllers
{
    [Route("api/MoreInformation")]
    [ApiController]
    public class MoreInformationController : ControllerBase
    {
        private readonly ILogger<MoreInformationController> _logger;
        private readonly ApplicationDbContext _context;

        public MoreInformationController(ILogger<MoreInformationController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        [HttpGet("AllPurchases")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            if (_context.Purchases == null)
            {
                return NotFound("Purchases not created yet!");
            }

            _logger.LogInformation("GETTING PURCHASES");
            return Ok(_context.Purchases);
        }



        [HttpGet("LastPurchase")]
        public IActionResult LastPurchase()
        {
            if (_context.Purchases == null)
            {
                return NotFound("Purchases not created yet!");
            }

            var lastPurchase = _context.Purchases.OrderBy(x => x.Id)
                                                 .LastOrDefault();
            

            _logger.LogInformation("LOADING LAST PURCHASE...");
            return Ok(lastPurchase);
        }
    }
}
