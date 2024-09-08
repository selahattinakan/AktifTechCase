using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AktifTech.API.Models
{
    public class CustomerModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("FullName")]
        [Required(ErrorMessage = "Lütfen müşteri adını giriniz.")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Lütfen geçerli bir mail giriniz.")]
        [Required(ErrorMessage = "Lütfen müşteri mailini giriniz.")]
        [JsonPropertyName("Mail")]
        public string Mail { get; set; }

        [Phone(ErrorMessage = "Lütfen geçerli bir telefon giriniz.")]
        [Required(ErrorMessage = "Lütfen müşteri telefonunu giriniz.")]
        [JsonPropertyName("Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Lütfen müşteri adresini giriniz.")]
        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Lütfen müşteri şifresini giriniz.")]
        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}
