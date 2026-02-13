using DataAccessLayer.Data.Models;
using DataAccessLayer.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Dtos.Orders
{
    public class GetOrderResponceDto
    {
        public Decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Status Status { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}
