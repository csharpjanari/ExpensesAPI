

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



        [HttpGet("GetPurchase/{purchaseId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetPurchase(int purchaseId)
        {
            if(purchaseId == 0)
            {
                _logger.LogError("ID == 0");
                return BadRequest("Id can't 0!!!");
            }

            var purchase = _context.Purchases.FirstOrDefault(x => x.Id == purchaseId);

            if(purchase == null)
            {
                _logger.LogError("PURCHASE == NULL");
                return NotFound("Error 404");
            }

            _logger.LogInformation("GETTING PURCHASE...");
            return Ok(purchase);
        }



        [HttpGet("Get/{sectionId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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



        [HttpPost("Create/{sectionId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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



        [HttpPut("Update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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



        [HttpPatch("UpdatePartial/{purchaseId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]
        public IActionResult UpdatePartial(int purchaseId, [FromBody] JsonPatchDocument<Purchase> patchDTO)
        {
            if(purchaseId == 0)
            {
                _logger.LogError("PURCHASE == 0");
                return BadRequest("Purchase can't be 0");
            }

            var purchase = _context.Purchases.FirstOrDefault(x => x.Id == purchaseId);

            if(purchase == null)
            {
                _logger.LogError("PURCHASE == NULL");
                return NotFound("Error 404");
            }

            patchDTO.ApplyTo(purchase, ModelState);

            if(!ModelState.IsValid)
            {
                _logger.LogError("ERROR - " + ModelState);
                return BadRequest(ModelState);
            }

            if (purchase.Сurrency == "USD" ||
               purchase.Сurrency == "EUR" ||
               purchase.Сurrency == "Tenge" ||
               purchase.Сurrency == "Ruble" ||
               purchase.Сurrency == "Manat" ||
               purchase.Сurrency == "Ien" ||
               purchase.Сurrency == "Funt")
            {
                _logger.LogInformation("Currency is good.");
            }
            else
            {
                _logger.LogError("Currency != good");
                return BadRequest("Currsency can be (USD, EUR, Tenge, Ruble, Manat, Ien, Funt!!!");
            }

            _context.Update(purchase);
            _context.SaveChanges();

            _logger.LogInformation("Saving Changes...");
            return Ok("Changes has been saved!");
        }



        [HttpDelete("Delete{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
