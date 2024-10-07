using Redis.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Redis
{
    public class Mutation
    {
        private readonly IDatabase _database;

        public Mutation(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<Product> AddProduct(Product product)
        {
            var productJson = JsonSerializer.Serialize(product);
            await _database.StringSetAsync($"product:{product.Id}", productJson);
            return product;
        }
    }
}
