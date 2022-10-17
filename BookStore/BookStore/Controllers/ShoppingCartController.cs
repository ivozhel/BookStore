using BookStore.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("GetContent")]
        public async Task<IActionResult> GetContent(int userId)
        {
            return Ok(await _shoppingCartService.GetContent(userId));
        }
        [HttpDelete("EmptyCart")]
        public async Task<IActionResult> EmptyCart(int userId)
        {
            await _shoppingCartService.EmptyCart(userId);
            return Ok();
        }
        [HttpPost("FinishPurchase")]
        public async Task<IActionResult> FinishPurchase(int userId)
        {
            await _shoppingCartService.FinishPurchase(userId);
            return Ok();
        }
        [HttpPut("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart(int bookId,int userId)
        {
            await _shoppingCartService.RemoveFromCart(bookId, userId);
            return Ok();
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int bookId,int userId)
        {
            await _shoppingCartService.AddToCart(bookId, userId);
            return Ok();
        }

    }
}
