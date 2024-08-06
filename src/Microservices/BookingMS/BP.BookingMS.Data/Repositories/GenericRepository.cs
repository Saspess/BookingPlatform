using System.Linq.Expressions;
using BP.BookingMS.Data.Contexts.Contracts;
using BP.BookingMS.Data.Entities.Contracts;
using BP.BookingMS.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BP.BookingMS.Data.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        protected IApplicationDbContext appContext;

        private DbSet<T> _dbSet { get; }

        public GenericRepository(IApplicationDbContext appContext)
        {
            this.appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
            _dbSet = appContext.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity, bool saveChanges = true)
        {
            var created = await _dbSet.AddAsync(entity);

            if (saveChanges)
            {
                await appContext.SaveChangesAsync();
            }

            return created.Entity;
        }

        public async Task UpdateAsync(T entity, bool saveChanges = true)
        {
            _dbSet.Update(entity);

            if (saveChanges)
            {
                await appContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id, bool saveChanges = true)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);

            if (saveChanges)
            {
                await appContext.SaveChangesAsync();
            }
        }
    }
}
