using BP.AccountsMS.Data.Entities;

namespace BP.AccountsMS.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(UserEntity userEntity);
    }
}
