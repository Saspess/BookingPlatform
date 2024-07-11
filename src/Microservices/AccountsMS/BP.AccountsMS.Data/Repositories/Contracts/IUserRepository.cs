using BP.AccountsMS.Data.Entities;

namespace BP.AccountsMS.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByEmailAsync(string email);
        Task<Guid> CreateAsync(UserEntity userEntity);
    }
}
