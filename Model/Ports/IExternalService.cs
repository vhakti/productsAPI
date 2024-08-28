using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Ports
{
    public interface IExternalService
    {
        Task<Double?> GetDiscountProduct(int productId);
    }
}
