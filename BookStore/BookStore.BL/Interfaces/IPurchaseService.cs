using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase?> SavePurchase(Purchase purchase);
        Task<Guid> DeletePurchase(Purchase purchase);
        Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int userId);
    }
}
