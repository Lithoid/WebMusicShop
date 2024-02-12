using BL;
using System.Threading.Tasks;
using System;

namespace Services.IServices
{
    public interface IOrderService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAllForUserAsync<T>(Guid userId,string token);
        Task<T> GetAsync<T>(Guid id, string token);
        Task<T> CreateAsync<T>(OrderViewModel orderModel, string token);
        Task<T> DeleteAsync<T>(Guid id, string token);
        Task<T> UpdateAsync<T>(OrderViewModel orderModel, string token);
    }
}
