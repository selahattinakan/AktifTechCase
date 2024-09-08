using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AktifTech.Database.Entity
{
    public class OrderProduct
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("CustomerOrderId")]
        [Required(ErrorMessage = "Lütfen müşteri bilgisi zorunludur.")]
        public int CustomerOrderId { get; set; }

        [JsonPropertyName("ProductId")]
        [Required(ErrorMessage = "Lütfen ürün bilgisi zorunludur.")]
        public int ProductId { get; set; }

        [JsonPropertyName("Quantity")]
        [Required(ErrorMessage = "Lütfen miktar bilgisi zorunludur.")]
        public int Quantity { get; set; }

        //[JsonPropertyName("CustomerOrder")]
        //public CustomerOrder CustomerOrder { get; set; }

        //[JsonPropertyName("Product")]
        //public Product Product { get; set; }
    }
}
