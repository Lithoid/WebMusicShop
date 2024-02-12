
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Domain;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using BL;
using Services.IServices;
using Polly;

namespace Services
{
    public class BaseService : IBaseService
    {
        public APIResponce responseModel { get; set ; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            responseModel = new();
            this.httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                
                var client = httpClient.CreateClient("StoreApi");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponseMessage = null;

                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",apiRequest.Token );
                }

                string apiContent = "";
                var policy = Policy.Handle<HttpRequestException>().RetryAsync(3);
                await policy.ExecuteAsync(async () =>
                {
                    apiResponseMessage = await client.SendAsync(message);
                    apiContent = await apiResponseMessage.Content.ReadAsStringAsync();
                });

             


                try
                {
                    APIResponce ApiResponse = JsonConvert.DeserializeObject<APIResponce>(apiContent);
                    if (apiResponseMessage.StatusCode==System.Net.HttpStatusCode.BadRequest ||
                        apiResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        ApiResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }
                }
                catch (Exception)
                {

                    var exeptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exeptionResponse;
                }
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse ;



            }
            catch (Exception e)
            {
                var dto = new APIResponce
                {
                    ErrorMessage = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false,
                };

                var res = JsonConvert.SerializeObject(dto);
                var APIResponse= JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
