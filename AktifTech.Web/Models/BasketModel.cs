namespace AktifTech.Web.Models
{
    public class BasketModel
    {
        public int Id { get; set; } 
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
