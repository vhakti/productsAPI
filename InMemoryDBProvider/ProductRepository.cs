using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDBProvider
{
    public class ProductRepository:GenericRepo<Product>
    {
        public ProductRepository(ProductContext context):base(context)
        {
            
        }
    }
}
