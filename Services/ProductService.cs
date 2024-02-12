using BL;
using Domain;
using Entities;
using Microsoft.Extensions.Configuration;
using Services.IServices;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace Services
{
    public class ProductService:BaseService, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string storeUrl { get; set; }

        public ProductService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            storeUrl = SD.ApiUrl;
        }

        public Task<T> CreateAsync<T>(ProductViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = storeUrl + "/api/ProductApi",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = storeUrl + "/api/ProductApi/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/ProductApi",
                //Token = token
            });
        }

        public Task<T> GetAllByCategoryAsync<T>(Guid categoryId,string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/ProductApi/GetProductsByCategory/"+ categoryId,
                //Token = token
            });
        }
       
        public Task<T> GetAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/ProductApi/" + id,
               // Token = token
            });
        }

        public Task<T> UpdateAsync<T>(ProductViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = storeUrl + "/api/ProductApi/" + dto.Id,
                //Token = token
            });
        }

        public Task<T> GetFavouriteForUser<T>(Guid userId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/ProductApi/GetFavouriteForUser/" + userId,
                //Token = token
            });
        }
    }
}
