using AktifTech.Constant;
using AktifTech.Database.Entity;
using AktifTech.Web.ApiService.Interfaces;
using AktifTech.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace AktifTech.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IApiProductService _apiProductService;

        public BasketController(IApiProductService apiProductService)
        {
            _apiProductService = apiProductService;
        }

        public async Task<IActionResult> Index()
        {
            string basket = Request.Cookies["Basket"];
            List<BasketModel> basketList = new List<BasketModel>();

            if (basket != null)
            {
                basketList = JsonSerializer.Deserialize<List<BasketModel>>(Encryption.Decrypt(basket));
            }

            List<BasketProductModel> basketProductList = new List<BasketProductModel>();

            foreach (var item in basketList)
            {
                Product product = await _apiProductService.GetProductAsync(item.ProductId);
                basketProductList.Add(new BasketProductModel
                {
                    Id = Encryption.Encrypt(product.Id.ToString()),
                    Name = product.Name,
                    Barkod = product.Barcode,
                    Description = product.Description,
                    Stock = product.Quantity,
                    Price = product.Price,
                    Quantity = item.Quantity
                });
            }
            return View(basketProductList);
        }

        [HttpPost]
        public IActionResult AddToBasket(BasketProductModel model)
        {
            int productID = Int32.Parse(Encryption.Decrypt(model.Id));
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString());

            string basket = Request.Cookies["Basket"];
            List<BasketModel> basketList = new List<BasketModel>();

            if (basket != null)
            {
                basketList = JsonSerializer.Deserialize<List<BasketModel>>(Encryption.Decrypt(basket));
            }

            if (basketList.Any(x => x.ProductId == productID && x.CustomerId == userId))
            {
                basketList.FirstOrDefault(x => x.ProductId == productID && x.CustomerId == userId).Quantity += model.Quantity;
            }
            else
            {
                basketList.Add(new BasketModel { ProductId = productID, CustomerId = userId, Quantity = model.Quantity });
            }

            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("Basket", Encryption.Encrypt(JsonSerializer.Serialize(basketList)), cookies);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult ConfirmBasket(Dictionary<string, int> Quantities)
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString());

            List<BasketModel> basketList = new List<BasketModel>();
            foreach (var item in Quantities)
            {
                int productId = Int32.Parse(Encryption.Decrypt(item.Key));
                int quantity = item.Value;

                basketList.Add(new BasketModel { ProductId = productId, CustomerId = userId, Quantity = quantity });    
            }

            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("Basket", Encryption.Encrypt(JsonSerializer.Serialize(basketList)), cookies);

            return RedirectToAction("Index", "Home");
        }
    }
}
