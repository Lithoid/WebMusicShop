using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IFavouriteService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetForUserAsync<T>(Guid userId, string token);
        Task<T> CreateAsync<T>(FavouriteViewModel model, string token);
        Task<T> DeleteAsync<T>(Guid prodId, Guid userId, string token);
        Task<T> GetFavouriteForUser<T>(Guid userId, string token);

    }
}
