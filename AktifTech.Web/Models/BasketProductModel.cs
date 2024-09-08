namespace AktifTech.Web.Models
{
    public class BasketProductModel
    {
        public string Id { get; set; }
        public int Quantity { get; set; }

        public string Name { get; set; }
        public string Barkod { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
