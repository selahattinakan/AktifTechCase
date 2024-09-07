using AktifTech.Constant;
using AktifTech.Database.Entity;
using AktifTech.Web.ApiService.Interfaces;

namespace AktifTech.Web.ApiService
{
    public class ApiProductService : ApiBaseService, IApiProductService
    {
        private const string BaseUrl = "https://localhost:7143/api/products";

        public ApiProductService(HttpClient httpClient) : base(httpClient)
        {
            httpClient = new HttpClient();
        }

        public async Task<ResultSet> DeleteProductAsync(int id)
        {
            return await DeleteAsync(id, BaseUrl);
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await GetAsync<Product>(id, BaseUrl);
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            return await GetListAsync<Product>(BaseUrl);
        }

        public async Task<ResultSet> SaveProductAsync(Product product)
        {
            return await SaveAsync(product, BaseUrl);
        }

        public async Task<ResultSet> UpdateProductAsync(Product product)
        {
            return await UpdateAsync(product, BaseUrl);
        }
    }
}
