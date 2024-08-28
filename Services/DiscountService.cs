using Model.DTOs;
using Model.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DiscountService : IExternalService
    {
        HttpClient _client;
        public DiscountService(HttpClient client)
        {
            _client = client;
        }
        public async Task<double?> GetDiscountProduct(int productId)
        {
            try
            {
                var discountProduct= await _client.GetFromJsonAsync<DiscountDto>($"/api/v1/discounts/{productId}");
                return discountProduct?.Discount;

            }
            catch (Exception exc)
            {
              
                return null;
            }
        }
    }
}
