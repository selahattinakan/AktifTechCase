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

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResultSet> DeleteProductAsync(Product product)
        {
            return await _productRepository.DeleteProductAsync(product);
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await _productRepository.GetProductAsync(id);
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            return await _productRepository.GetProductListAsync();
        }

        public async Task<ResultSet> SaveProductAsync(Product product)
        {
            return await _productRepository.SaveProductAsync(product);
        }

        public async Task<ResultSet> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateProductAsync(product);
        }
    }
}
