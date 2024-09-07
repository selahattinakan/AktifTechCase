using AktifTech.API.Authentication.Models;

namespace AktifTech.API.Authentication.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginUserAsync(string userName);
    }
}
