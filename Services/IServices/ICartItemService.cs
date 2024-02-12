using BL;
using System.Threading.Tasks;
using System;

namespace Services.IServices
{
    public interface ICartItemService
    {
        Task<T> GetAllAsync<T>(Guid cartId, string token);
        Task<T> GetAsync<T>(Guid id, string token);
        Task<T> CreateAsync<T>(CartViewModel cartModel, string token);
        Task<T> DeleteAsync<T>(Guid id, string token);
        Task<T> UpdateAsync<T>(CartViewModel cartModel, string token);
    }
}
