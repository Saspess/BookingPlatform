using BP.AccountsMS.Data.Entities;

namespace BP.AccountsMS.Data.Repositories.Contracts
{
    public interface IAccountRepository
    {
        Task<AccountEntity> GetByIdAsync(Guid id);
        Task<AccountEntity> GetByEmailAsync(string email);
        Task<Guid> CreateAsync(AccountEntity userEntity);
        Task UpdateAsync(AccountEntity accountEntity);
    }
}
