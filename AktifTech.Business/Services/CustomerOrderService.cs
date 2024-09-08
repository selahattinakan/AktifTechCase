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
        private readonly IProductRepository _productRepository;
        private readonly IRabbitMQPublishService _rabbitMQPublishService;
        //decortor desing pattern uygulanacak

        public CustomerOrderService(ICustomerOrderRepository customerOrderRepository, IProductRepository productRepository, IRabbitMQPublishService rabbitMQPublishService)
        {
            _customerOrderRepository = customerOrderRepository;
            _productRepository = productRepository;
            _rabbitMQPublishService = rabbitMQPublishService;
        }

        public async Task<ResultSet> ConfirmCustomerOrder(int id)
        {
            var result =  await _customerOrderRepository.ConfirmCustomerOrder(id);

            if (result.Result == Result.Success)
            {
                var customerOder = await _customerOrderRepository.GetCustomerOrderAsync(id);
                string message = $"{DateTime.Now} : Siparişiniz alınmıştır. Sipariş numaranız: {customerOder.Id}";

                _rabbitMQPublishService.Publish(message);
            }
            return result;
        }

        public async Task<ResultSet> DeleteCustomerOrderAsync(CustomerOrder customerOrder)
        {
            return await _customerOrderRepository.DeleteCustomerOrderAsync(customerOrder);
        }

        public async Task<List<CustomerOrder>?> GetCustomerListOrderAsync(int customerId)
        {
            return await _customerOrderRepository.GetCustomerListOrderAsync(customerId);
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

        public async Task<ResultSet> UpdateCustomerOrderProductAsync(CustomerOrder customerOrder)
        {
            foreach (var item in customerOrder.OrderProducts)
            {
                var product = await _productRepository.GetProductAsync(item.ProductId);
                if (product.Quantity < item.Quantity)
                {
                    ResultSet resultSet = new ResultSet
                    {
                        Result = Result.Fail,
                        Message = "Stok yetersiz."
                    };
                    return resultSet;
                }
            }

            return await _customerOrderRepository.UpdateCustomerOrderAsync(customerOrder);
        }
    }
}
