using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Dtos.Products
{
    public class AddProductDto
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }

}
