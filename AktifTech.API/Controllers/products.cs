using AktifTech.Business.Interfaces;
using AktifTech.Business.Services;
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
    public class products : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<products> _logger;

        public products(IProductService productService, ILogger<products> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]// test datası oluşturabilmek için
        public async Task<IActionResult> GetProductList()
        {
            _logger.LogInformation("Tüm ürünlerin listelenmesi için istek alındı.");

            List<Product> productList = await _productService.GetProductListAsync();
            return Ok(productList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            _logger.LogInformation("Ürün bilgisi için istek alındı.");

            Product? product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Ürün için bilgi bulunamadı. id:{id}");
                return NotFound(new { message = "Kayıt bulunamadı." });
            }

            return Ok(product);
        }

        [HttpPost]
        [AllowAnonymous]// test datası oluşturabilmek için
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            _logger.LogInformation("Ürün kaydı için istek alındı.");

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

                _logger.LogWarning($"Ürün kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            result = await _productService.SaveProductAsync(product);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Ürün kaydı başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            _logger.LogInformation("Ürün güncelleme için istek alındı.");

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

                _logger.LogWarning($"Ürün güncelleme başarısız oldu. {result.Message}");

                return BadRequest(result);
            }
            else if (product.Id <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";

                _logger.LogWarning($"Ürün güncelleme başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            result = await _productService.UpdateProductAsync(product);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Ürün güncelleme başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation("Ürün silme için istek alındı.");

            ResultSet result = new ResultSet();
            Product? product = await _productService.GetProductAsync(id);

            if (product == null)
            {
                result.Result = Result.Fail;
                result.Message = "Geçerli bir kayıt bulunamadı.";

                _logger.LogWarning($"Ürün silme başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            result = await _productService.DeleteProductAsync(product);
            if (result.Result == Result.Fail)
            {
                _logger.LogWarning($"Ürün silme başarısız oldu. {result.Message}");

                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
