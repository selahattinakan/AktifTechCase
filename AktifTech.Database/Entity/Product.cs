using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AktifTech.Database.Entity
{
    public class Product
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        [Required(ErrorMessage ="Lütfen ürün adını giriniz.")]
        public string Name { get; set; }

        [JsonPropertyName("Barcode")]
        [Required(ErrorMessage = "Lütfen barkod numarasını giriniz.")]
        public string Barcode { get; set; }

        [JsonPropertyName("Description")]
        [Required(ErrorMessage = "Lütfen ürün tanımını giriniz.")]
        public string Description { get; set; }

        [JsonPropertyName("Quantity")]
        [Required(ErrorMessage = "Lütfen ürün miktarını giriniz.")]
        public int Quantity { get; set; }

        [JsonPropertyName("Price")]
        [Required(ErrorMessage = "Lütfen ürün fiyatını giriniz.")]
        public decimal Price { get; set; }
    }
}
