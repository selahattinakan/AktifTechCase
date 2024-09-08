using AktifTech.Business.Interfaces;
using AktifTech.Cache.Repositories;
using AktifTech.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Business.Services
{
    public class RedisService : IRedisService
    {
        private readonly RedisRepository _redisRepository;

        public RedisService(RedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        public async Task<bool> CreateProductListCache(List<Product> products)
        {
            return await _redisRepository.CreateProductListCache(products);
        }

        public async Task<List<Product>> GetProductListFromCache()
        {
            return await _redisRepository.GetProductListFromCache();
        }
    }
}
