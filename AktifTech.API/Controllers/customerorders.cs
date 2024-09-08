using AktifTech.API.Models;
using AktifTech.Business.Interfaces;
using AktifTech.Business.Services;
using AktifTech.Constant;
using AktifTech.Database.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AktifTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class customerorders : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;
        private readonly IProductService _productService;
        private readonly ILogger<customerorders> _logger;

        public customerorders(ICustomerOrderService customerOrderService, ILogger<customerorders> logger, IProductService productService)
        {
            _customerOrderService = customerOrderService;
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerOrderById(int id)
        {
            _logger.LogInformation("Müşteri sipariş bilgisi için istek alındı.");

            CustomerOrder? customerOrder = await _customerOrderService.GetCustomerOrderAsync(id);
            var test = customerOrder.OrderProducts;
            if (customerOrder == null)
            {
                _logger.LogWarning($"Müşteri sipariş için bilgi bulunamadı. id:{id}");

                return NotFound(new { message = "Kayıt bulunamadı." });
            }

            return Ok(customerOrder);
        }

        [HttpGet("{id}/customers")]
        public async Task<IActionResult> GetCustomerOrderByCustomerId(int id)
        {
            _logger.LogInformation("Müşteri sipariş bilgisi için istek alındı.");

            List<CustomerOrder>? customerOrderList = await _customerOrderService.GetCustomerListOrderAsync(id);
            if (customerOrderList == null)
            {
                _logger.LogWarning($"Müşteri sipariş için bilgi bulunamadı. id:{id}");

                return NotFound(new { message = "Kayıt bulunamadı." });
            }

            return Ok(customerOrderList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerOrder([FromBody] CustomerOrderModel customerOrder)
        {
            _logger.LogInformation("Müşteri sipariş kaydı için istek alındı.");

            ResultSet result = new ResultSet();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                result.Result = Result.Fail;
                result.Message = "Lütfen zorunlu alanları doldurun";
                result.Object = errors;

                _logger.LogWarning($"Müşteri sipariş kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            CustomerOrder customerOrderData = new CustomerOrder
            {
                Address = customerOrder.Address,
                CustomerId = customerOrder.CustomerId,
                OrderDate = customerOrder.OrderDate
            };

            result = await _customerOrderService.SaveCustomerOrderAsync(customerOrderData);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri sipariş kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("/orderproduct")]
        public async Task<IActionResult> CreateOrderProducts([FromBody] List<OrderProductModel> orderProducts)
        {
            _logger.LogInformation("Müşteri sipariş kaydı için istek alındı.");

            ResultSet result = new ResultSet();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                result.Result = Result.Fail;
                result.Message = "Lütfen zorunlu alanları doldurun";
                result.Object = errors;

                _logger.LogWarning($"Müşteri sipariş kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }
            else if (orderProducts.Count == 0)
            {
                result.Result = Result.Fail;
                result.Message = "Lütfen zorunlu alanları doldurun";

                _logger.LogWarning($"Müşteri sipariş kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            int customerOrderId = orderProducts.FirstOrDefault().CustomerOrderId;
            CustomerOrder? customerOrder = await _customerOrderService.GetCustomerOrderAsync(customerOrderId);

            //customerOrder!.OrderProducts = new List<OrderProduct>();

            foreach (var orderProduct in orderProducts)
            {
                OrderProduct orderProductData = new OrderProduct
                {
                    CustomerOrderId = orderProduct.CustomerOrderId,
                    ProductId = orderProduct.ProductId,
                    Quantity = orderProduct.Quantity
                };

                if (!customerOrder.OrderProducts.Any(x => x.ProductId == orderProduct.ProductId))
                {
                    customerOrder.OrderProducts.Add(orderProductData);
                }
                else
                {
                    customerOrder.OrderProducts.FirstOrDefault(x => x.ProductId == orderProduct.ProductId).Quantity += orderProduct.Quantity;
                }
            }

            result = await _customerOrderService.UpdateCustomerOrderProductAsync(customerOrder);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri sipariş kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerOrder([FromBody] CustomerOrderModel customerOrder)
        {
            _logger.LogInformation("Müşteri sipariş güncellemesi için istek alındı.");

            ResultSet result = new ResultSet();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                result.Result = Result.Fail;
                result.Message = "Lütfen zorunlu alanları doldurun";
                result.Object = errors;

                _logger.LogWarning($"Müşteri sipariş güncellemesi başarısız oldu. {result.Message}");

                return BadRequest(result);
            }
            else if (customerOrder.Id <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";

                _logger.LogWarning($"Müşteri sipariş güncellemesi başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            CustomerOrder customerOrderData = new CustomerOrder
            {
                Id = customerOrder.Id,
                Address = customerOrder.Address,
                CustomerId = customerOrder.CustomerId,
                OrderDate = customerOrder.OrderDate
            };

            result = await _customerOrderService.UpdateCustomerOrderAsync(customerOrderData);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri sipariş güncellemesi başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerOrder(int id)
        {
            _logger.LogInformation("Müşteri sipariş silinmesi için istek alındı.");

            ResultSet result = new ResultSet();
            CustomerOrder? customerOrder = await _customerOrderService.GetCustomerOrderAsync(id);

            if (customerOrder == null)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";

                _logger.LogWarning($"Müşteri sipariş silmesi başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            result = await _customerOrderService.DeleteCustomerOrderAsync(customerOrder);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri sipariş silmesi başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{customerOrderId}/orderproducts")]
        public async Task<IActionResult> DeleteCustomerOrderProduct(int customerOrderId)
        {
            _logger.LogInformation("Müşteri sipariş silinmesi için istek alındı.");

            ResultSet result = new ResultSet();
            CustomerOrder? customerOrder = await _customerOrderService.GetCustomerOrderAsync(customerOrderId);
            customerOrder!.OrderProducts = new List<OrderProduct>();

            if (customerOrder == null)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";

                _logger.LogWarning($"Müşteri sipariş silmesi başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            result = await _customerOrderService.UpdateCustomerOrderAsync(customerOrder);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri sipariş silmesi başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("{id}/confirmcustomerorder")]
        public async Task<IActionResult> ConfirmCustomerOrder(int id)
        {
            _logger.LogInformation("Müşteri sipariş onaylanması için istek alındı.");

            ResultSet result = await _customerOrderService.ConfirmCustomerOrder(id);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri sipariş için bilgi bulunamadı. id:{id}");

                return NotFound(new { message = "Kayıt bulunamadı." });
            }

            return Ok("Sipariş onaylandı");
        }
    }
}
