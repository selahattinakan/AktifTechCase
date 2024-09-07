using AktifTech.API.Authentication.Models;

namespace AktifTech.API.Authentication.Interfaces
{
    public interface ITokenService
    {
        public Task<TokenResponse> GenerateToken(TokenRequest request);
    }
}
