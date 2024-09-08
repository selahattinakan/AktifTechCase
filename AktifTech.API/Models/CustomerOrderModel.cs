using AktifTech.Database.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AktifTech.API.Models
{
    public class CustomerOrderModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("CustomerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("OrderDate")]
        public DateTime OrderDate { get; set; }

        //[JsonPropertyName("Customer")]
        //[NotMapped]
        //public  CustomerModel? Customer { get; set; }

        //[JsonPropertyName("OrderProducts")]
        //public List<OrderProductModel>? OrderProducts { get; set; }
    }
}
