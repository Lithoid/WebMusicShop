using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAssetService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(Guid id, string token);
        Task<T> CreateAsync<T>(AssetViewModel assetModel, string token);
        Task<T> DeleteAsync<T>(Guid id, string token);
        Task<T> UpdateAsync<T>(AssetViewModel assetModel, string token);
    }
}
