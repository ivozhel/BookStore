using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<Book>> GetContent(int userId);
        Task AddToCart(int bookId,int userId);
        Task RemoveFromCart(int bookId, int userId);
        Task EmptyCart(int userId);
        Task FinishPurchase(int userId);
    }
}
