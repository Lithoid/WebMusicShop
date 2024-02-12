using BL;
using System;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IBrandService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(Guid id, string token);
        Task<T> CreateAsync<T>(BrandViewModel brandModel, string token);
        Task<T> DeleteAsync<T>(Guid id, string token);
        Task<T> UpdateAsync<T>(BrandViewModel brandModel, string token);
    }
}
