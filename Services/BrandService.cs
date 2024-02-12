using BL;
using Domain;
using Microsoft.Extensions.Configuration;
using Services.IServices;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace Services
{
    public class BrandService : BaseService, IBrandService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string storeUrl { get; set; }

        public BrandService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            storeUrl = SD.ApiUrl;

            // storeUrl = configuration.GetValue<string>("ServiceUrls:MusicStoreApi");
        }

        public Task<T> CreateAsync<T>(BrandViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = storeUrl + "/api/BrandApi",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = storeUrl + "/api/BrandApi/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/BrandApi",
                //Token = token
            });
        }

        public Task<T> GetAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/BrandApi/" + id,
               // Token = token
            });
        }

        public Task<T> UpdateAsync<T>(BrandViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = storeUrl + "/api/BrandApi/" + dto.Id,
                //Token = token
            });
        }
    }
}
