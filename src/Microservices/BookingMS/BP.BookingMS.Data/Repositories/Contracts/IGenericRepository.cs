using System.Linq.Expressions;
using BP.BookingMS.Data.Entities.Contracts;

namespace BP.BookingMS.Data.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<IList<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<T, object>>[] includes);

        Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<T, object>>[] includes);

        Task<T> CreateAsync(T entity, bool saveChanges = true);
        Task UpdateAsync(T entity, bool saveChanges = true);
        Task DeleteAsync(Guid id, bool saveChanges = true);
    }
}
