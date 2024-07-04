using BP.IdentityMS.Data.Entities;
using BP.IdentityMS.Data.Repositories.Contracts;
using BP.IdentityMS.Data.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BP.IdentityMS.Data.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> _collection;

        public UserRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> dbSettings)
        {
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _collection = database.GetCollection<UserEntity>(dbSettings.Value.CollectionName);
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _collection.Find(e => true).ToListAsync();
        }

        public async Task<UserEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(UserEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(UserEntity entity)
        {
            var existingEntity = Builders<UserEntity>
                .Filter
                .Where(e => e.Id == entity.Id);

            var updateEntity = Builders<UserEntity>
                .Update
                .Set(e => e.Email, entity.Email)
                .Set(e => e.Password, entity.Password);

            await _collection.UpdateOneAsync(existingEntity, updateEntity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.FindOneAndDeleteAsync(e => e.Id == id);
        }
    }
}
