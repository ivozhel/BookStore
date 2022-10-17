using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IPurchaseRepo
    {
        Task<Purchase?> SavePurchase (Purchase purchase);
        Task<Guid> DeletePurchase(Purchase purchase);
        Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int userId);
    }
}
