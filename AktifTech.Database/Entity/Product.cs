using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Database.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Lütfen ürün adını giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen barkod numarasını giriniz.")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "Lütfen ürün tanımını giriniz.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Lütfen ürün miktarını giriniz.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Lütfen ürün fiyatını giriniz.")]
        public decimal Price { get; set; }
    }
}
