using AktifTech.API.Authentication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AktifTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authentications : ControllerBase
    {
        private readonly IAuthService _authService;

        public authentications(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                var result = await _authService.LoginUserAsync(username);
                return Ok(new { result.AuthToken });
            }

            return Unauthorized();
        }
    }
}
