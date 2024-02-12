using BL;
using Domain;
using Microsoft.Extensions.Configuration;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FavouriteService : BaseService, IFavouriteService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string storeUrl { get; set; }

        public FavouriteService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            storeUrl = SD.ApiUrl;
        }

        public Task<T> GetFavouriteForUser<T>(Guid userId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/FavouriteApi/GetFavouriteForUser/" + userId,
                //Token = token
            });
        }

        public Task<T> CreateAsync<T>(FavouriteViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = storeUrl + "/api/FavouriteApi",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(Guid prodId, Guid userId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = storeUrl + "/api/FavouriteApi/" + prodId+"/"+ userId,
                //Token = token
            });
        }

 
        public Task<T> GetForUserAsync<T>(Guid userId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/FavouriteApi/"+userId,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = storeUrl + "/api/FavouriteApi" ,
                //Token = token
            });
        }

       
    }
}
