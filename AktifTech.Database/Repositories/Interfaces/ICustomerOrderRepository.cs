using AktifTech.Constant;
using AktifTech.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Database.Repositories.Interfaces
{
    public interface ICustomerOrderRepository
    {
        public Task<CustomerOrder?> GetCustomerOrderAsync(int id);
        public Task<List<CustomerOrder>?> GetCustomerListOrderAsync(int customerId);
        public Task<ResultSet> SaveCustomerOrderAsync(CustomerOrder customerOrder);
        public Task<ResultSet> UpdateCustomerOrderAsync(CustomerOrder customerOrder);
        public Task<ResultSet> DeleteCustomerOrderAsync(CustomerOrder customerOrder);
        public Task<ResultSet> ConfirmCustomerOrder(int id);
    }
}
