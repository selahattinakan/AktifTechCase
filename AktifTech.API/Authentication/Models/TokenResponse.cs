namespace AktifTech.API.Authentication.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
    }
}
