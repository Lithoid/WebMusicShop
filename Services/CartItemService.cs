using BL;
using Domain;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;

using Services.IServices;

namespace Services
{
    public class CartItemService : BaseService, ICartItemService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string storeUrl { get; set; }

        public CartItemService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            storeUrl = SD.ApiUrl;
           // storeUrl = configuration.GetValue<string>("ServiceUrls:MusicStoreApi");
        }

        public Task<T> CreateAsync<T>(CartViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = storeUrl + "/api/CartItemApi",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = storeUrl + "/api/CartItemApi/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>(Guid cartId,string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/CartItemApi/GetCartItems/" + cartId,
                //Token = token
            });
        }

        public Task<T> GetAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/CartItemApi/GetCartItem/" + id,
                // Token = token
            });
        }

        public Task<T> UpdateAsync<T>(CartViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = storeUrl + "/api/CartItemApi/" + dto.Id,
                //Token = token
            });
        }
    }
}
