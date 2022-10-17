using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStore.DL.Repositories.MongoRepos
{
    public class PurchaseRepository : IPurchaseRepo
    {
        private readonly IMongoCollection<Purchase> _mongoCollection;
        private readonly IOptions<MongoPurchaseConfiguration> _mongoConfig;

        public PurchaseRepository(IOptions<MongoPurchaseConfiguration> mongoConfig)
        {
            _mongoConfig = mongoConfig;
            var mongoClient = new MongoClient(_mongoConfig.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoConfig.Value.DatabaseName);
            _mongoCollection = mongoDatabase.GetCollection<Purchase>(
                _mongoConfig.Value.CollectionName);

        }

        public async Task<Guid> DeletePurchase(Purchase purchase)
        {
            await _mongoCollection.DeleteOneAsync(x => x.Id == purchase.Id);
            return purchase.Id;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int userId)
        {
            try
            {
                var result = await _mongoCollection.FindAsync(x => x.UserId == userId);
                return await result.ToListAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Purchase?> SavePurchase(Purchase purchase)
        {
            await _mongoCollection.InsertOneAsync(purchase);
            return purchase;

        }
    }
}
