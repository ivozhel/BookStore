using BookStore.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet("GetAllPurchases")]
        public async Task<IActionResult> GetAllPurchasesForUser(int id)
        {
            return Ok(await _purchaseService.GetAllPurchasesForUser(id));
        }


    }
}
