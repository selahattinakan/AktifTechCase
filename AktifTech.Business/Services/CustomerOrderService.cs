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
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;

        public CustomerOrderService(ICustomerOrderRepository customerOrderRepository)
        {
            _customerOrderRepository = customerOrderRepository;
        }

        public async Task<ResultSet> DeleteCustomerOrderAsync(CustomerOrder customerOrder)
        {
            return await _customerOrderRepository.DeleteCustomerOrderAsync(customerOrder);
        }

        public async Task<CustomerOrder?> GetCustomerOrderAsync(int id)
        {
            return await _customerOrderRepository.GetCustomerOrderAsync(id);
        }

        public async Task<ResultSet> SaveCustomerOrderAsync(CustomerOrder customerOrder)
        {
            return await _customerOrderRepository.SaveCustomerOrderAsync(customerOrder);
        }

        public async Task<ResultSet> UpdateCustomerOrderAsync(CustomerOrder customerOrder)
        {
            return await _customerOrderRepository.UpdateCustomerOrderAsync(customerOrder);
        }
    }
}
