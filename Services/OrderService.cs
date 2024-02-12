using BL;
using Domain;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Services.IServices;
using Entities;

namespace Services
{
    public class OrderService:BaseService, IOrderService
    { 
        private readonly IHttpClientFactory _clientFactory;
        private string storeUrl { get; set; }

        public OrderService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            storeUrl = SD.ApiUrl;
        }

        public Task<T> CreateAsync<T>(OrderViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = storeUrl + "/api/OrderApi",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = storeUrl + "/api/OrderApi/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/OrderApi",
                //Token = token
            });
        }

        public Task<T> GetAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/OrderApi/" + id,
                // Token = token
            });
        }

        public Task<T> UpdateAsync<T>(OrderViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = storeUrl + "/api/OrderApi/" + dto.Id,
                //Token = token
            });
        }

        public Task<T> GetAllForUserAsync<T>(Guid userId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/OrderApi/GetOrdersForUser/" + userId,
                //Token = token
            });
        }
    }
}
