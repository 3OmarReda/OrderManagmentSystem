using DataAccessLayer.Data.Models.Enums;

namespace DataAccessLayer.Data.Models
{
    public class Order : BaseEntity
    {
        public Decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Status Status { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public Invoice Invoice { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}