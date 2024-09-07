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
    public class products : ControllerBase
    {
        private readonly IProductService _productService;

        public products(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            List<Product> productList = await _productService.GetProductListAsync();
            return Ok(productList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            Product? product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Kayıt bulunamadı." });
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
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

            result = await _productService.SaveProductAsync(product);
            if (result.Result == Result.Fail)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
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
            else if (product.Id <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";
                return BadRequest(result);
            }

            result = await _productService.UpdateProductAsync(product);
            if (result.Result == Result.Fail)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ResultSet result = new ResultSet();
            Product? product = await _productService.GetProductAsync(id);

            if (product == null)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";
                return BadRequest(result);
            }

            result = await _productService.DeleteProductAsync(product);
            if (result.Result == Result.Fail)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
