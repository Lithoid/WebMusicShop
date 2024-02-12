using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IDbRepository<T>
        where T : class, IDbEntity
    {
        IQueryable<T> AllItems { get; }

        Task<List<T>> GetAllItemsAsync(Expression<Func<T, bool>>? filter = null, string? includeProps = null);

        Task<bool> AddItemAsync(T item, bool saving = true);

        Task<int> AddItemsAsync(IEnumerable<T> items);

        Task<T> GetItemAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProps = null);

        Task<bool> ChangeItemAsync(T item);

        Task<bool> DeleteItemAsync(Guid id);
        Task<bool> DeleteItemsAsync(IEnumerable<T> entities);

        Task<int> SaveChangesAsync();
    }
}