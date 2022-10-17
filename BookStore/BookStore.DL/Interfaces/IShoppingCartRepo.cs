using BookStore.Models.Models;
using BookStore.Models.Models.Users;

namespace BookStore.DL.Interfaces
{
    public interface IShoppingCartRepo
    {
        public Task AddPurchasToCart(ShoppingCart purchase);
        public Task<ShoppingCart> GetCart(int userId);
        public Task Delete(int userId);
        public Task<ShoppingCart> Update(ShoppingCart purchase, int userId);
    }
}
