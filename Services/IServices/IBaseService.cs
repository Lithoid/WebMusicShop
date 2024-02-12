
using BL;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IBaseService
    {
        APIResponce responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest aPIRequest);


    }
}
