using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDatabase _database;

        public ProductController(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var productJson = await _database.StringGetAsync($"product:{id}");
            if (productJson.IsNull)
            {
                return NotFound();
            }

            var product = JsonSerializer.Deserialize<Product>(productJson);
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            var productJson = JsonSerializer.Serialize(product);
            await _database.StringSetAsync($"product:{product.Id}", productJson);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    }
}
