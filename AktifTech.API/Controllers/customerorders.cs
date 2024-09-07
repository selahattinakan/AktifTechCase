using AktifTech.Business.Interfaces;
using AktifTech.Business.Services;
using AktifTech.Constant;
using AktifTech.Database.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AktifTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customerorders : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;
        private readonly ILogger<customerorders> _logger;

        public customerorders(ICustomerOrderService customerOrderService, ILogger<customerorders> logger)
        {
            _customerOrderService = customerOrderService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerOrderById(int id)
        {
            _logger.LogInformation("Müşteri sipariş bilgisi için istek alındı.");

            CustomerOrder? customerOrder = await _customerOrderService.GetCustomerOrderAsync(id);
            if (customerOrder == null)
            {
                _logger.LogWarning($"Müşteri sipariş için bilgi bulunamadı. id:{id}");

                return NotFound(new { message = "Kayıt bulunamadı." });
            }

            return Ok(customerOrder);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerOrder([FromBody] CustomerOrder customerOrder)
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

            result = await _customerOrderService.SaveCustomerOrderAsync(customerOrder);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri sipariş kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerOrder([FromBody] CustomerOrder customerOrder)
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

            result = await _customerOrderService.UpdateCustomerOrderAsync(customerOrder);
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
    }
}
