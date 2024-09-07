using AktifTech.Constant;
using AktifTech.Database.Entity;

namespace AktifTech.Web.ApiService.Interfaces
{
    public interface IApiCustomerOrderService
    {
        public Task<CustomerOrder?> GetCustomerOrderAsync(int id);
        public Task<ResultSet> SaveCustomerOrderAsync(CustomerOrder customerOrder);
        public Task<ResultSet> UpdateCustomerOrderAsync(CustomerOrder customerOrder);
        public Task<ResultSet> DeleteCustomerOrderAsync(int id);
    }
}
