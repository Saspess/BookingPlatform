using System.Linq.Expressions;
using BP.IdentityMS.Data.Entities;
using MongoDB.Driver;

namespace BP.IdentityMS.Data.Repositories.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<string> CreateAsync(TEntity entity);
        Task UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);
        Task DeleteAsync(string id);
    }
}
