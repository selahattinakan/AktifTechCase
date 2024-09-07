using AktifTech.Constant;
using AktifTech.Database.Entity;
using AktifTech.Web.ApiService.Interfaces;

namespace AktifTech.Web.ApiService
{
    public class ApiCustomerService : ApiBaseService, IApiCustomerService
    {
        private const string BaseUrl = "https://localhost:7143/api/customers";

        public ApiCustomerService(HttpClient httpClient) : base(httpClient)
        {
            httpClient = new HttpClient();
        }

        public async Task<ResultSet> DeleteCustomerAsync(int id)
        {
            return await DeleteAsync(id, BaseUrl);
        }

        public async Task<Customer?> GetCustomerAsync(int id)
        {
            return await GetAsync<Customer>(id, BaseUrl);
        }

        public async Task<ResultSet> SaveCustomerAsync(Customer customer)
        {
            return await SaveAsync(customer, BaseUrl);
        }

        public async Task<ResultSet> UpdateCustomerAsync(Customer customer)
        {
            return await UpdateAsync(customer, BaseUrl);
        }
    }
}
