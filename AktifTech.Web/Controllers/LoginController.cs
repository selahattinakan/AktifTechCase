using AktifTech.Web.ApiService.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AktifTech.Web.Models;
using AktifTech.Database.Entity;

namespace AktifTech.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IApiCustomerService _apiCustomerService;

        public LoginController(IApiCustomerService apiCustomerService)
        {
            _apiCustomerService = apiCustomerService;
        }

        public IActionResult Index()
        {
            return View(true);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserModel model)
        {
            Customer customer = await _apiCustomerService.LoginAsync(model.UserName, model.Password);
            if (customer != null && customer?.Id > 0)
            {
                List<Claim> claims = new List<Claim>()
                    {
                        new Claim (ClaimTypes.Name, customer.Mail),
                        new Claim (ClaimTypes.NameIdentifier, customer.Id.ToString())
                    };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }
            return View(false);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
