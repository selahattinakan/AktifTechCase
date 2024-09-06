using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Database.Entity
{
    public class CustomerOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen müşteri bilgisi zorunludur.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Lütfen ürün bilgisi zorunludur.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Lütfen adres bilgisi zorunludur.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Lütfen miktar bilgisi zorunludur.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Lütfen fiyat bilgisi zorunludur.")]
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
