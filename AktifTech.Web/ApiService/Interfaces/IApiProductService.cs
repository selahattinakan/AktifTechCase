using AktifTech.Constant;
using AktifTech.Database.Entity;

namespace AktifTech.Web.ApiService.Interfaces
{
    public interface IApiProductService
    {
        public Task<Product?> GetProductAsync(int id);
        public Task<ResultSet> SaveProductAsync(Product product);
        public Task<ResultSet> UpdateProductAsync(Product product);
        public Task<ResultSet> DeleteProductAsync(int id);
    }
}
