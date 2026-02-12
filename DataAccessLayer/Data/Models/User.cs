using DataAccessLayer.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Customer;

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }

}
