using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Model.DTOs;
using Model.Ports;
using Services;
namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
       
        public ProductController(IProductService service)
        {
            _productService = service;
          
            

        }

        [HttpGet("get_product_by_id")]
        public async Task<IActionResult> GetById(int id)
        {
             var productDto =  await _productService.GetProductById(id);
                return Ok(productDto);
           
        }
        [HttpPost("create_product")]
        public async Task<int?> Insert(CreateProductDto product)
        {
            return await _productService.CreateProduct(product);
        }

        [HttpPut]
        public async Task<bool> Update(UpdateProductDto product)
        {
            return await _productService.UpdateProduct(product);
        }


    }
}
