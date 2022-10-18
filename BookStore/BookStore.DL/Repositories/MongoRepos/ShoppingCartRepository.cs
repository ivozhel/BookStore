using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using BookStore.Models.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStore.DL.Repositories.MongoRepos
{
    public class ShoppingCartRepository : IShoppingCartRepo
    {
        private readonly IMongoCollection<ShoppingCart> _mongoCollection;
        private readonly IOptions<MongoShoppingCart> _mongoConfig;

        public ShoppingCartRepository(IOptions<MongoShoppingCart> mongoConfig)
        {
            _mongoConfig = mongoConfig;
            var mongoClient = new MongoClient(_mongoConfig.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoConfig.Value.DatabaseName);
            _mongoCollection = mongoDatabase.GetCollection<ShoppingCart>(
                _mongoConfig.Value.CollectionName);

        }
        public async Task AddPurchasToCart(ShoppingCart purchase)
        {
            await _mongoCollection.InsertOneAsync(purchase);            
        }

        public async Task Delete(int userId)
        {
            await _mongoCollection.DeleteOneAsync(x=>x.UserId == userId);
        }

        public async Task<ShoppingCart> GetCart(int userId)
        {
            var result= await _mongoCollection.FindAsync(x=>x.UserId == userId);
            return result.FirstOrDefault();
        }

        public async Task<ShoppingCart> Update(ShoppingCart purchase,int userId)
        {
            var update = Builders<ShoppingCart>.Update
                .Set(p => p.Books, purchase.Books);
            return await _mongoCollection.FindOneAndUpdateAsync(x => x.UserId == userId, update);
        }
    }
}
