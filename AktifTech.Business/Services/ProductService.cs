using AktifTech.Business.Interfaces;
using AktifTech.Constant;
using AktifTech.Database.Entity;
using AktifTech.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRedisService _redisService;
        //decorator design pattern uygulanacak

        public ProductService(IProductRepository productRepository, IRedisService redisService)
        {
            _productRepository = productRepository;
            _redisService = redisService;
        }

        public async Task<ResultSet> DeleteProductAsync(Product product)
        {
            ResultSet result = await _productRepository.DeleteProductAsync(product);
            if (result.Result == Result.Success)
            {
                await _redisService.CreateProductListCache(await _productRepository.GetProductListAsync());
            }

            return result;
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await _productRepository.GetProductAsync(id);
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            List<Product> productList = await _redisService.GetProductListFromCache();
            return productList ?? await _productRepository.GetProductListAsync();
        }

        public async Task<ResultSet> SaveProductAsync(Product product)
        {
            ResultSet result = await _productRepository.SaveProductAsync(product);
            if (result.Result == Result.Success)
            {
                await _redisService.CreateProductListCache(await _productRepository.GetProductListAsync());
            }

            return result;
        }

        public async Task<ResultSet> UpdateProductAsync(Product product)
        {
            ResultSet result = await _productRepository.UpdateProductAsync(product);
            if (result.Result == Result.Success)
            {
                await _redisService.CreateProductListCache(await _productRepository.GetProductListAsync());
            }

            return result;
        }
    }
}
