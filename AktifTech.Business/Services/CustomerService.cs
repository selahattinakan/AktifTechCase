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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResultSet> DeleteCustomerAsync(Customer customer)
        {
            return await _customerRepository.DeleteCustomerAsync(customer);
        }

        public async Task<Customer?> GetCustomerAsync(int id)
        {
            return await _customerRepository.GetCustomerAsync(id);
        }

        public async Task<Customer?> LoginAsync(string mail, string password)
        {
            return await _customerRepository.LoginAsync(mail, password);
        }

        public async Task<ResultSet> SaveCustomerAsync(Customer customer)
        {
            customer.Password = Encryption.Encrypt(customer.Password);
            return await _customerRepository.SaveCustomerAsync(customer);
        }

        public async Task<ResultSet> UpdateCustomerAsync(Customer customer)
        {
            customer.Password = Encryption.Encrypt(customer.Password);
            return await _customerRepository.UpdateCustomerAsync(customer);
        }
    }
}
