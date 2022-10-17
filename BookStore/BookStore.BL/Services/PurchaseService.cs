using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepo _purchaseRepo;
        public PurchaseService(IPurchaseRepo purchaseRepo)
        {
            _purchaseRepo = purchaseRepo;
        }
        public async Task<Guid> DeletePurchase(Purchase purchase)
        {
            return await _purchaseRepo.DeletePurchase(purchase);
        }

        public Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int userId)
        {
            return _purchaseRepo.GetAllPurchasesForUser(userId);
        }

        public Task<Purchase?> SavePurchase(Purchase purchase)
        {
            return _purchaseRepo.SavePurchase(purchase);
        }
    }
}
