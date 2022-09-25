using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesAPI.Controllers
{
    [Route("api/Purchase")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;
        private readonly ApplicationDbContext _context;

        public PurchaseController(ILogger<PurchaseController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            if(_context.Purchases == null)
            {
                return NotFound("Purchases not created yet!");
            }

            _logger.LogInformation("GETTING PURCHASES");
            return Ok(_context.Purchases);
        }


        [HttpGet("Get/{sectionId}")]
        public IActionResult Get(int sectionId)
        {
            if(sectionId == 0)
            {
                _logger.LogError("ID == 0");
                return BadRequest("Id can't be 0.");
            }

            var category = _context.Purchases.Where(x => x.SectionId == sectionId);

            if(category == null)
            {
                _logger.LogError("ID == NULL");
                return NotFound("This section id is not found.");
            }

            _logger.LogInformation("GETTING PURCHASES OF SECTION_ID");
            return Ok(category);
        }


        [HttpPost("Create/{sectionId}")]
        public IActionResult Create(int sectionId, PurchaseDto purchase)
        {
            if(sectionId == 0)
            {
                _logger.LogError("ID == 0");
                return BadRequest("Id can't be 0.");
            }

            var newPurchase = new Purchase
            {
                Description = purchase.Description,
                HowMany = purchase.HowMany,
                Сurrency = purchase.Сurrency,
                SectionId = sectionId,
                WasBought = DateTime.Now,
                NameOfSection = _context.Sections.Find(sectionId).Name
            };

            if(newPurchase.Сurrency == "USD" ||
               newPurchase.Сurrency == "EUR" ||
               newPurchase.Сurrency == "Tenge" ||
               newPurchase.Сurrency == "Ruble" ||
               newPurchase.Сurrency == "Manat" ||
               newPurchase.Сurrency == "Ien" ||
               newPurchase.Сurrency == "Funt")
            {
                _logger.LogInformation("Currency is good.");
            }
            else
            {
                _logger.LogError("Currency != good");
                return BadRequest("Currsency can be (USD, EUR, Tenge, Ruble, Manat, Ien, Funt!!!");
            }

            _context.Purchases.Add(newPurchase);
            _context.SaveChanges();

            _logger.LogInformation("PURCHASE IS CREATING");
            return Ok("Purchase is created");
        }


        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, PurchaseDto purchase)
        {
            if(id == 0)
            {
                _logger.LogError("ID == 0");
                return BadRequest("Id can't be 0.");
            }

            var searchPurchase = _context.Purchases.FirstOrDefault(x => x.Id == id);

            if (searchPurchase == null)
            {
                _logger.LogError("PURCHASES == NULL");
                return NotFound("This purchase id is not found, see GetAll method, for information about purchase.");
            }

            searchPurchase.Description = purchase.Description;
            searchPurchase.HowMany = purchase.HowMany;
            searchPurchase.Сurrency = purchase.Сurrency;
            searchPurchase.SectionId = purchase.SectionId;
            searchPurchase.NameOfSection = _context.Sections.Find(purchase.SectionId).Name;

            if(searchPurchase.Сurrency == "USD" ||
               searchPurchase.Сurrency == "EUR" ||
               searchPurchase.Сurrency == "Tenge" ||
               searchPurchase.Сurrency == "Ruble" ||
               searchPurchase.Сurrency == "Manat" ||
               searchPurchase.Сurrency == "Ien" ||
               searchPurchase.Сurrency == "Funt")
            {
                _logger.LogInformation("Currency is good.");
            }
            else
            {
                _logger.LogError("Currency != good");
                return BadRequest("Currsency can be (USD, EUR, Tenge, Ruble, Manat, Ien, Funt!!!");
            }

            _context.SaveChanges();

            _logger.LogInformation("PURCHASE IS UPDATED!");
            return Ok("Purchase is updated.");
        }


        [HttpDelete("Delete{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                _logger.LogError("ID == 0");
                return BadRequest("Id can't be 0.");
            }

            var searchPurchase = _context.Sections.FirstOrDefault(c => c.Id == id);

            if(searchPurchase == null)
            {
                _logger.LogError("PURCHASE == NULL");
                return NotFound("Purchase is not found.");
            }

            _context.Remove(searchPurchase);
            _context.SaveChanges();

            _logger.LogInformation("PURCHASE IS DELETED!");
            return Ok("Purchase is deleted!");
        }
    }
}
