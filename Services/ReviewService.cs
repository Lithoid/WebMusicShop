using BL;
using Domain;
using Microsoft.Extensions.Configuration;
using Services.IServices;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace Services
{
    public class ReviewService : BaseService, IReviewService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string storeUrl { get; set; }

        public ReviewService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            storeUrl = SD.ApiUrl;

            // storeUrl = configuration.GetValue<string>("ServiceUrls:MusicStoreApi");
        }
        public Task<T> GetAllForProductAsync<T>(Guid productId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/ReviewApi/GetAllForProduct/" + productId,
                //Token = token
            });
        }
        public Task<T> GetAllForUserAsync<T>(Guid userId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/ReviewApi/GetReviewsForUser/" + userId,
                //Token = token
            });
        }
        public Task<T> CreateAsync<T>(ReviewViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = storeUrl + "/api/ReviewApi",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = storeUrl + "/api/ReviewApi/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/ReviewApi",
                //Token = token
            });
        }

        public Task<T> GetAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/ReviewApi/" + id,
               // Token = token
            });
        }

        public Task<T> UpdateAsync<T>(ReviewViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = storeUrl + "/api/ReviewApi/" + dto.Id,
                //Token = token
            });
        }
    }
}
