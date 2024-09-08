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
    public class OrderProductService : IOrderProductService
    {
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerOrderRepository _customerOrderRepository;

        public OrderProductService(IOrderProductRepository orderProductRepository, IProductRepository productRepository, ICustomerOrderRepository customerOrderRepository)
        {
            _orderProductRepository = orderProductRepository;
            _productRepository = productRepository;
            _customerOrderRepository = customerOrderRepository;
        }

        public async Task<ResultSet> DeleteOrderProductAsync(OrderProduct orderProduct)
        {
            return await _orderProductRepository.DeleteOrderProductAsync(orderProduct);
        }

        public async Task<OrderProduct?> GetOrderProductAsync(int id)
        {
            return await _orderProductRepository.GetOrderProductAsync(id);
        }

        public async Task<List<OrderProduct>> GetOrderProductListAsync()
        {
            return await _orderProductRepository.GetOrderProductListAsync();
        }

        public async Task<ResultSet> SaveOrderProductAsync(OrderProduct orderProduct)
        {
            ResultSet resultSet = new ResultSet();
            bool isExist = false;
            var customerOrder = await _customerOrderRepository.GetCustomerOrderAsync(orderProduct.CustomerOrderId);

            if (customerOrder == null)
            {
                resultSet.Result = Result.Fail;
                resultSet.Message = "Müşteri siparişi bulunamadı";
                return resultSet;
            }

            if (customerOrder.OrderProducts.Any(x => x.ProductId == orderProduct.ProductId))
            {
                orderProduct.Quantity += customerOrder.OrderProducts.FirstOrDefault(x => x.ProductId == orderProduct.ProductId).Quantity;
                isExist = true;
            }

            var product = await _productRepository.GetProductAsync(orderProduct.ProductId);

            if (product.Quantity < orderProduct.Quantity)
            {

                resultSet.Result = Result.Fail;
                resultSet.Message = "Stok yetersiz.";
                return resultSet;
            }

            if (isExist)
            {
                return await _orderProductRepository.UpdateOrderProductAsync(orderProduct);
            }
            else
            {
                return await _orderProductRepository.SaveOrderProductAsync(orderProduct);
            }
        }

        public async Task<ResultSet> UpdateOrderProductAsync(OrderProduct orderProduct)
        {
            ResultSet resultSet = new ResultSet();
            var customerOrder = await _customerOrderRepository.GetCustomerOrderAsync(orderProduct.CustomerOrderId);

            if (customerOrder == null)
            {
                resultSet.Result = Result.Fail;
                resultSet.Message = "Müşteri siparişi bulunamadı";
                return resultSet;
            }

            var product = await _productRepository.GetProductAsync(orderProduct.ProductId);

            if (product.Quantity < orderProduct.Quantity)
            {

                resultSet.Result = Result.Fail;
                resultSet.Message = "Stok yetersiz.";
                return resultSet;
            }

            return await _orderProductRepository.UpdateOrderProductAsync(orderProduct);
        }
    }
}
