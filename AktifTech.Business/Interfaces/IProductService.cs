using AktifTech.Constant;
using AktifTech.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Business.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetProductListAsync();
        public Task<Product?> GetProductAsync(int id);
        public Task<ResultSet> SaveProductAsync(Product product);
        public Task<ResultSet> UpdateProductAsync(Product product);
        public Task<ResultSet> DeleteProductAsync(Product product);
    }
}
