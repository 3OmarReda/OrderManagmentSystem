using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Models.Enums
{
    public enum Status
    {
        Pending = 0,    
        Confirmed = 1,  
        Shipped = 2,    
        Delivered = 3,  
        Cancelled = 4   
    }

}
