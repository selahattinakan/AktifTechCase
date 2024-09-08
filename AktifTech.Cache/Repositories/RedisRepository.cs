using AktifTech.Database.Entity;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AktifTech.Cache.Repositories
{
    public class RedisRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase db;
        private const int dbIndex = 1;
        private const string productListKey = "productListKey";
        private const int hashCode = 1234; // tekil bir kayıt tutsaydık, item id'sini kullanabilirdik.  bir test uygulaması olduğu için bu şekilde kullandım
        public RedisRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            db = _redis.GetDatabase(dbIndex);
        }

        public async Task<bool> CreateProductListCache(List<Product> productList)
        {
            try
            {
                return await db.HashSetAsync(productListKey, hashCode , JsonSerializer.Serialize(productList));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Product>?> GetProductListFromCache()
        {
            try
            {
                if (!db.KeyExists(productListKey)) return null;

                var productsJson = await db.HashGetAsync(productListKey, hashCode);
                return productsJson.HasValue ? JsonSerializer.Deserialize<List<Product>>(productsJson.ToString()) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
