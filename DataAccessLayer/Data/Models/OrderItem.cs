namespace DataAccessLayer.Data.Models
{
    public class OrderItem:BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int productId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quentity { get; set; }
        public decimal UnitPrice { get; set; }
        public int Discount { get; set; }
    }
}