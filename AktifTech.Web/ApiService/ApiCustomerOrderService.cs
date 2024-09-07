using AktifTech.Constant;
using AktifTech.Database.Entity;
using AktifTech.Web.ApiService.Interfaces;

namespace AktifTech.Web.ApiService
{
    public class ApiCustomerOrderService : ApiBaseService, IApiCustomerOrderService
    {
        private const string BaseUrl = "https://localhost:7143/api/customerorders";

        public ApiCustomerOrderService(HttpClient httpClient) : base(httpClient)
        {
            httpClient = new HttpClient();
        }

        public async Task<ResultSet> DeleteCustomerOrderAsync(int id)
        {
            return await DeleteAsync(id, BaseUrl);
        }

        public async Task<CustomerOrder?> GetCustomerOrderAsync(int id)
        {
            return await GetAsync<CustomerOrder>(id, BaseUrl);
        }

        public async Task<ResultSet> SaveCustomerOrderAsync(CustomerOrder customerOrder)
        {
            return await SaveAsync(customerOrder, BaseUrl);
        }

        public async Task<ResultSet> UpdateCustomerOrderAsync(CustomerOrder customerOrder)
        {
            return await UpdateAsync(customerOrder, BaseUrl);
        }
    }
}
