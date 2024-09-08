using AktifTech.Constant;
using AktifTech.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Business.Interfaces
{
    public interface ICustomerService
    {
        public Task<Customer?> LoginAsync(string mail, string password);
        public Task<Customer?> GetCustomerAsync(int id);
        public Task<List<Customer>> GetCustomerListAsync();
        public Task<ResultSet> SaveCustomerAsync(Customer customer);
        public Task<ResultSet> UpdateCustomerAsync(Customer customer);
        public Task<ResultSet> DeleteCustomerAsync(Customer customer);
    }
}
