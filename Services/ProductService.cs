using InMemoryDBProvider;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Model;
using Model.DTOs;
using Model.Ports;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Services
{
 

    public class ProductService : IProductService
    {
        private readonly ProductRepository _repo;
        private readonly IExternalService _extService;
        private readonly IMemoryCache _memCache;

        public ProductService(ProductRepository repository, IExternalService extService,IMemoryCache cache)
        {
            this._repo = repository;
            this._extService = extService;
            this._memCache = cache;


        }
        public async Task<int?> CreateProduct(CreateProductDto product)
        {
            try
            {
                var newProduct = new Product()
                {
                    Name = product.ProductName,
                    Description = product.ProductDescription,
                    CreatedOn = DateTime.Now,
                    Price = product.ProductPrice,
                    Status = (int)StatusTypeEnum.Active,
                    Stock = product.StockUnits

                };
                return await this._repo.Insert(newProduct);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating product: {product.ProductName}");
               
            }
        }

        public async Task<GetProductDto?> GetProductById(int id)
        {
            try
            {
                ConcurrentDictionary<int, string> status_prod;
                _memCache.TryGetValue($"status_product", out status_prod);
                
                    var disc = await this._extService.GetDiscountProduct(id);
                var product = await this._repo.GetById(id);
                if (product != null)
                {
                    return new GetProductDto()
                    {
                        ProductId = product.Id,
                        Description = product.Description,
                        Name = product.Name,
                        Price = product.Price,
                        Stock = product.Stock,
                        StatusName = status_prod==null?((StatusTypeEnum)product.Status).ToString(): status_prod.Where(s=>s.Key==product.Status).Select(v=>v.Value).FirstOrDefault(),
                        FinalPrice = disc !=null? (double)(product.Price * (100 -disc)/100):(double)product.Price,
                        Discount = disc ?? 0.0
                    };
                }
                else
                    return null;
            }
            catch (Exception ex)
            {                
               throw new Exception($"GetProductById failed with error: {ex.Message}");
                
            }
}

        public async Task<bool> UpdateProduct(UpdateProductDto product)
        {
            try { 
                var productToUpdate = new Product()
                {
                    Name = product.ProductName,
                    Description = product.ProductDescription,
                    EditedOn = DateTime.Now,
                    Id = product.Id,
                    Price = product.ProductPrice,
                    Status = product.Status,
                    Stock = product.StockUnits
                };
                await this._repo.Update(productToUpdate);
                return true;
            }
            catch (Exception ex)
            {
               throw new Exception($"UpdateProduct failed with error: {ex.Message}");
                
            }

        }
    }
}
