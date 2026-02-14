using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Dtos.Customers
{
    public class AddCustomerDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

}
