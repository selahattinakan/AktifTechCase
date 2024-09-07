using AktifTech.Constant;
using AktifTech.Database.Entity;
using AktifTech.Database.Repositories.Interfaces;
using AktifTech.Web.ApiService.Interfaces;
using AktifTech.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AktifTech.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiCustomerService _apiCustomerService;
        private readonly IApiProductService _apiProductService;

        public HomeController(ILogger<HomeController> logger, IApiCustomerService apiCustomerService, IApiProductService apiProductService)
        {
            _logger = logger;
            _apiCustomerService = apiCustomerService;
            _apiProductService = apiProductService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> InitialData()
        {
            #region TestData
            Customer customer = new Customer
            {
                FullName = "Selahattin Akan",
                Mail = "sakan@gmail.com",
                Phone = "5079613188",
                Address = "Nalçacý Cd 43/8 Konya",
                Password = "123456"
            };
            Customer customer2 = new Customer
            {
                FullName = "Lütfiye Akan",
                Mail = "lakan@gmail.com",
                Phone = "5079613188",
                Address = "Nalçacý Cd 43/8 Konya",
                Password = "123456"
            };

            Product product = new Product
            {
                Name = "Mavi Kalem",
                Barcode = "1111111",
                Description = "Mavi renkte yazan kalem",
                Price = 10,
                Quantity = 100
            };

            Product product2 = new Product
            {
                Name = "Siyah Kalem",
                Barcode = "2222222",
                Description = "Siyah renkte yazan kalem",
                Price = 15,
                Quantity = 100
            };

            Product product3 = new Product
            {
                Name = "Yeþil Kalem",
                Barcode = "3333333",
                Description = "Yeþil renkte yazan kalem",
                Price = 5,
                Quantity = 100
            };

            Product product4 = new Product
            {
                Name = "Beyaz Silgi",
                Barcode = "4444444",
                Description = "Beyaz renkte silgi",
                Price = 20,
                Quantity = 100
            };

            Product product5 = new Product
            {
                Name = "Siyah Silgi",
                Barcode = "5555555",
                Description = "Siyah renkte silgi",
                Price = 10,
                Quantity = 100
            };

            Product product6 = new Product
            {
                Name = "Defter",
                Barcode = "6666666",
                Description = "Yazý yazabileceðiniz defter",
                Price = 30,
                Quantity = 100
            };

            Product product7 = new Product
            {
                Name = "Not Defteri",
                Barcode = "77777777",
                Description = "Not yazabileceðiniz defter",
                Price = 40,
                Quantity = 100
            };

            Product product8 = new Product
            {
                Name = "Kalemlik",
                Barcode = "88888888",
                Description = "Kalemleri koyabileceðiniz kalemlik",
                Price = 15,
                Quantity = 100
            };

            Product product9 = new Product
            {
                Name = "Okul Çantasý",
                Barcode = "9999999",
                Description = "Eþyalarýnýzý koyabileceðiniz çanta",
                Price = 100,
                Quantity = 100
            };

            var res1 = await _apiCustomerService.SaveCustomerAsync(customer);
            var res2 = await _apiCustomerService.SaveCustomerAsync(customer2);

            var res3 = await _apiProductService.SaveProductAsync(product);
            var res4 = await _apiProductService.SaveProductAsync(product2);
            var res5 = await _apiProductService.SaveProductAsync(product3);
            var res6 = await _apiProductService.SaveProductAsync(product4);
            var res7 = await _apiProductService.SaveProductAsync(product5);
            var res8 = await _apiProductService.SaveProductAsync(product6);
            var res9 = await _apiProductService.SaveProductAsync(product7);
            var res10 = await _apiProductService.SaveProductAsync(product8);
            var res11 = await _apiProductService.SaveProductAsync(product9); 
            #endregion

            return Content("Test datalarý hazýrlandý");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
