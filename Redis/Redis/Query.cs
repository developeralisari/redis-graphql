using Redis.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Redis
{
    public class Query
    {
        private readonly IDatabase _database;

        public Query(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<Product> GetProduct(string id)
        {
            var productJson = await _database.StringGetAsync($"product:{id}");
            return productJson.IsNull ? null : JsonSerializer.Deserialize<Product>(productJson);
        }
    }
}
