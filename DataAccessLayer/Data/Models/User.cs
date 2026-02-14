using DataAccessLayer.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Models
{
    public class User : IdentityUser
    {
        public UserRole Role { get; set; } = UserRole.Customer;
        public string RoleName => Role.ToString();
    }

}
