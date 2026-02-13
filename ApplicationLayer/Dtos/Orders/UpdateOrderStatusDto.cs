using DataAccessLayer.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Dtos.Orders
{
    public class UpdateOrderStatusDto
    {
        public Status Status { get; set; }
    }
}
