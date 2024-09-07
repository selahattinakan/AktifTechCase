using AktifTech.Constant;
using AktifTech.Database.Entity;

namespace AktifTech.Web.ApiService.Interfaces
{
    public interface IApiCustomerService
    {
        public Task<Customer?> GetCustomerAsync(int id);
        public Task<ResultSet> SaveCustomerAsync(Customer customer);
        public Task<ResultSet> UpdateCustomerAsync(Customer customer);
        public Task<ResultSet> DeleteCustomerAsync(int id);
    }
}
