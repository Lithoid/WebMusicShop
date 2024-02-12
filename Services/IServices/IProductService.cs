using BL;
using System;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IProductService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(Guid id, string token);
        Task<T> CreateAsync<T>(ProductViewModel productModel, string token);
        Task<T> DeleteAsync<T>(Guid id, string token);
        Task<T> UpdateAsync<T>(ProductViewModel productModel, string token);
        Task<T> GetAllByCategoryAsync<T>(Guid categoryId, string token);

        Task<T> GetFavouriteForUser<T>(Guid userId, string token);


        
    }
}
