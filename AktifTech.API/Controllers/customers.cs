using AktifTech.API.Authentication.Interfaces;
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
    public class customers : ControllerBase
    {
        private readonly ICustomerService _customerService;
       // private readonly IAuthService _authService;

        public customers(ICustomerService customerService)
        {
            _customerService = customerService;
            //_authService = authService;
        }


        //[HttpPost("{mail}/{password}")]
        //public async Task<IActionResult> LoginControl(string mail, string password)
        //{
        //    Customer? customer = await _customerService.LoginAsync(mail, Encryption.Encrypt(password));
        //    if (customer == null)
        //    {
        //        return NotFound(new { message = "Kayıt bulunamadı." });
        //    }

        //    var result = await _authService.LoginUserAsync(mail);

        //    return Ok(result);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            Customer? customer = await _customerService.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound(new { message = "Kayıt bulunamadı." });
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
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

            result = await _customerService.SaveCustomerAsync(customer);
            if (result.Result == Result.Fail)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
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
            else if (customer.Id <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";
                return BadRequest(result);
            }

            result = await _customerService.UpdateCustomerAsync(customer);
            if (result.Result == Result.Fail)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            ResultSet result = new ResultSet();
            Customer? customer = await _customerService.GetCustomerAsync(id);

            if (customer == null)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";
                return BadRequest(result);
            }

            result = await _customerService.DeleteCustomerAsync(customer);
            if (result.Result == Result.Fail)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }



    }
}
