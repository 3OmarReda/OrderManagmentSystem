using DataAccessLayer.Data.Models;
using DataAccessLayer.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Dtos.Orders
{
    public class AddOrderDto
    {
        public Guid CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }

}
