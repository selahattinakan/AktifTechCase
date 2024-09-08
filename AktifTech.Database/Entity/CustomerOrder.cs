using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AktifTech.Database.Entity
{
    public class CustomerOrder
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("CustomerId")]
        [Required(ErrorMessage = "Lütfen müşteri bilgisi zorunludur.")]
        public int CustomerId { get; set; }

        [JsonPropertyName("Address")]
        [Required(ErrorMessage = "Lütfen adres bilgisi zorunludur.")]
        public string Address { get; set; }

        [JsonPropertyName("OrderDate")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("Customer")]
        public Customer? Customer { get; set; }

        [JsonPropertyName("OrderProducts")]
        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
