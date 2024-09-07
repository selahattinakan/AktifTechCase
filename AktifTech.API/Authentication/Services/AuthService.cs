using AktifTech.API.Authentication.Interfaces;
using AktifTech.API.Authentication.Models;

namespace AktifTech.API.Authentication.Services
{
    public class AuthService : IAuthService
    {
        readonly ITokenService tokenService;

        public AuthService(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        public async Task<LoginResponse> LoginUserAsync(string userName)
        {
            LoginResponse response = new();

            var generatedTokenInformation = await tokenService.GenerateToken(new TokenRequest { Username = userName });

            response.AuthenticateResult = true;
            response.AuthToken = generatedTokenInformation.Token;
            response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;

            return response;
        }
    }
}
