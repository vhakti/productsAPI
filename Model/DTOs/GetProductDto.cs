using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class GetProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusName { get; set; }
        public int Stock { get; set; }  
        public double Price { get; set; }
        public double Discount { get; set; }
        public double FinalPrice { get; set; }
    }
}
