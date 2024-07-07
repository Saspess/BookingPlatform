using System.Linq.Expressions;
using BP.IdentityMS.Data.Entities;
using BP.IdentityMS.Data.Repositories.Contracts;
using BP.IdentityMS.Data.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BP.IdentityMS.Data.Repositories
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IMongoCollection<TEntity> collection;

        public GenericRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> dbSettings)
        {
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            collection = database.GetCollection<TEntity>(dbSettings.Value.CollectionName);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return await collection.Find(filter).ToListAsync();
            }

            return await collection.Find(e => true).ToListAsync();
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return await collection.Find(filter).FirstOrDefaultAsync();
            }

            return await collection.Find(e => true).FirstOrDefaultAsync();
        }

        public virtual async Task<string> CreateAsync(TEntity entity)
        {
            await collection.InsertOneAsync(entity);
            return entity.Id;
        }

        public virtual async Task UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            await collection.UpdateOneAsync(filter, update);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await collection.FindOneAndDeleteAsync(e => e.Id == id);
        }
    }
}
