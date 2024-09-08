using AktifTech.API.Authentication.Interfaces;
using AktifTech.API.Models;
using AktifTech.Business.Interfaces;
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
    public class customers : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<customers> _logger;

        public customers(ICustomerService customerService, ILogger<customers> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost("{mail}/{password}")]
        public async Task<IActionResult> LoginControl(string mail, string password)
        {
            Customer? customer = await _customerService.LoginAsync(mail, Encryption.Encrypt(password));
            if (customer == null)
            {
                return NotFound(new { message = "Kayıt bulunamadı." });
            }
            return Ok(customer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            _logger.LogInformation("Müşteri bilgisi için istek alındı.");

            Customer? customer = await _customerService.GetCustomerAsync(id);
            if (customer == null)
            {
                _logger.LogWarning($"Müşteri için bilgi bulunamadı. id:{id}");
                return NotFound(new { message = "Kayıt bulunamadı." });
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerModel customerModel)
        {
            _logger.LogInformation("Müşteri kaydı için istek alındı.");

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

                _logger.LogWarning($"Müşteri kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            Customer customer = new Customer
            {
                FullName = customerModel.FullName,
                Mail = customerModel.Mail,
                Password = Encryption.Encrypt(customerModel.Password),
                Phone = customerModel.Phone,
                Address = customerModel.Address
            };

            result = await _customerService.SaveCustomerAsync(customer);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerModel customerModel)
        {
            _logger.LogInformation("Müşteri güncelleme için istek alındı.");

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

                _logger.LogWarning($"Müşteri güncelleme başarısız oldu. {result.Message}");

                return BadRequest(result);
            }
            else if (customerModel.Id <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";

                _logger.LogWarning($"Müşteri güncelleme başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            Customer customer = new Customer
            {
                Id = customerModel.Id,
                FullName = customerModel.FullName,
                Mail = customerModel.Mail,
                Password = Encryption.Encrypt(customerModel.Password),
                Phone = customerModel.Phone,
                Address = customerModel.Address
            };

            result = await _customerService.UpdateCustomerAsync(customer);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri güncelleme başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            _logger.LogInformation("Müşteri silme için istek alındı.");

            ResultSet result = new ResultSet();
            Customer? customer = await _customerService.GetCustomerAsync(id);

            if (customer == null)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";

                _logger.LogWarning($"Müşteri silme başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            result = await _customerService.DeleteCustomerAsync(customer);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Müşteri silme başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }



    }
}
