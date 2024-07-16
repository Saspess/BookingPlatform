using BP.AccountsMS.Data.Entities;

namespace BP.AccountsMS.Data.Repositories.Contracts
{
    public interface IOneTimePasswordRepository
    {
        Task<OneTimePasswordEntity> GetActiveAsync();
        Task<Guid> CreateAsync(OneTimePasswordEntity oneTimePasswordEntity);
        Task DeactivateAsync(Guid accountId);
    }
}
