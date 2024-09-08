using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AktifTech.API.Models
{
    public class ProductModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Barcode")]
        public string Barcode { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("Price")]
        public decimal Price { get; set; }
    }
}
