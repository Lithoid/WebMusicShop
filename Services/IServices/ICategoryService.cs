using BL;
using System;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ICategoryService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(Guid id, string token);
        Task<T> CreateAsync<T>(CategoryViewModel categoryModel, string token);
        Task<T> DeleteAsync<T>(Guid id, string token);
        Task<T> UpdateAsync<T>(CategoryViewModel categoryModel, string token);
    }
}
