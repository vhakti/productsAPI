using Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Ports
{
    public interface IProductService
    {
        Task<GetProductDto?> GetProductById(int id);
        Task<int?> CreateProduct(CreateProductDto product);
        Task<bool> UpdateProduct(UpdateProductDto product);
    }
}
