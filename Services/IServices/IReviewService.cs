using BL;
using System;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IReviewService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(Guid id, string token);
        Task<T> CreateAsync<T>(ReviewViewModel reviewModel, string token);
        Task<T> DeleteAsync<T>(Guid id, string token);
        Task<T> UpdateAsync<T>(ReviewViewModel reviewModel, string token);
        Task<T> GetAllForProductAsync<T>(Guid productId, string token);

        Task<T> GetAllForUserAsync<T>(Guid userId, string token);
    }
}
