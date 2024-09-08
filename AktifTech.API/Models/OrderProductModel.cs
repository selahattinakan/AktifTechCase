using AktifTech.Database.Entity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AktifTech.API.Models
{
    public class OrderProductModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("CustomerOrderId")]
        public int CustomerOrderId { get; set; }

        [JsonPropertyName("ProductId")]
        public int ProductId { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        //[JsonPropertyName("CustomerOrder")]
        //public CustomerOrderModel CustomerOrder { get; set; }

        //[JsonPropertyName("Product")]
        //public ProductModel Product { get; set; }
    }
}
