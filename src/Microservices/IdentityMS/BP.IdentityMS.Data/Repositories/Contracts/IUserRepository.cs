using BP.IdentityMS.Data.Entities;

namespace BP.IdentityMS.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task<UserEntity> GetByIdAsync(string id);
        Task CreateAsync(UserEntity entity);
        Task UpdateAsync(UserEntity entity);
        Task DeleteAsync(string id);
    }
}
