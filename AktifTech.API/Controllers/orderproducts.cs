using AktifTech.API.Models;
using AktifTech.Business.Interfaces;
using AktifTech.Constant;
using AktifTech.Database.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AktifTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class orderproducts : ControllerBase
    {
        private readonly IOrderProductService _orderProductService;
        private readonly ILogger<orderproducts> _logger;

        public orderproducts(IOrderProductService orderProductService, ILogger<orderproducts> logger)
        {
            _orderProductService = orderProductService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderProductById(int id)
        {
            _logger.LogInformation("Müşteri sipariş bilgisi için istek alındı.");

            OrderProduct? orderProduct = await _orderProductService.GetOrderProductAsync(id);
            if (orderProduct == null)
            {
                _logger.LogWarning($"Müşteri sipariş için bilgi bulunamadı. id:{id}");

                return NotFound(new { message = "Kayıt bulunamadı." });
            }

            return Ok(orderProduct);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderProduct([FromBody] OrderProductModel orderProductModel)
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

            OrderProduct orderProduct = new OrderProduct
            {
                CustomerOrderId = orderProductModel.CustomerOrderId,
                ProductId = orderProductModel.ProductId,
                Quantity = orderProductModel.Quantity
            };

            result = await _orderProductService.SaveOrderProductAsync(orderProduct);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri sipariş kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOrderProduct([FromBody] OrderProductModel orderProductModel)
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
            else if (orderProductModel.Id <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";

                _logger.LogWarning($"Müşteri sipariş güncellemesi başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            OrderProduct orderProduct = new OrderProduct
            {
                Id = orderProductModel.Id,
                CustomerOrderId = orderProductModel.CustomerOrderId,
                ProductId = orderProductModel.ProductId,
                Quantity = orderProductModel.Quantity
            };

            result = await _orderProductService.UpdateOrderProductAsync(orderProduct);
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
            OrderProduct? orderProduct = await _orderProductService.GetOrderProductAsync(id);

            if (orderProduct == null)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";

                _logger.LogWarning($"Müşteri sipariş silmesi başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            result = await _orderProductService.DeleteOrderProductAsync(orderProduct);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri sipariş silmesi başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
