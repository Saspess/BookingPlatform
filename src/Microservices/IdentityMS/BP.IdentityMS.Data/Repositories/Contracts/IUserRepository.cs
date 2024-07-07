using BP.IdentityMS.Data.Entities;

namespace BP.IdentityMS.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task<UserEntity> GetByIdAsync(string id);
        Task<string> CreateAsync(UserEntity entity);
        Task UpdateAsync(UserEntity entity);
        Task DeleteAsync(string id);
    }
}
