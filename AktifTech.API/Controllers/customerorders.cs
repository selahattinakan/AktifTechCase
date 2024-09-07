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

        public customerorders(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerOrderById(int id)
        {
            CustomerOrder? customerOrder = await _customerOrderService.GetCustomerOrderAsync(id);
            if (customerOrder == null)
            {
                return NotFound(new { message = "Kayıt bulunamadı." });
            }

            return Ok(customerOrder);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerOrder([FromBody] CustomerOrder customerOrder)
        {
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
                return BadRequest(result);
            }

            result = await _customerOrderService.SaveCustomerOrderAsync(customerOrder);
            if (result.Result == Result.Fail)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerOrder([FromBody] CustomerOrder customerOrder)
        {
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
                return BadRequest(result);
            }
            else if (customerOrder.Id <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";
                return BadRequest(result);
            }

            result = await _customerOrderService.UpdateCustomerOrderAsync(customerOrder);
            if (result.Result == Result.Fail)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerOrder(int id)
        {
            ResultSet result = new ResultSet();
            CustomerOrder? customerOrder = await _customerOrderService.GetCustomerOrderAsync(id);

            if (customerOrder == null)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";
                return BadRequest(result);
            }

            result = await _customerOrderService.DeleteCustomerOrderAsync(customerOrder);
            if (result.Result == Result.Fail)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
