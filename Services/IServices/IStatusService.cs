using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IStatusService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(string name, string token);
        Task<T> CreateAsync<T>(StatusViewModel productModel, string token);
        Task<T> DeleteAsync<T>(Guid id, string token);
        Task<T> UpdateAsync<T>(StatusViewModel productModel, string token);
    }
}
