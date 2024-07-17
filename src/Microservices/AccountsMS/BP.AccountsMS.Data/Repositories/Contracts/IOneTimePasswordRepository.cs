using BP.AccountsMS.Data.Entities;

namespace BP.AccountsMS.Data.Repositories.Contracts
{
    public interface IOneTimePasswordRepository
    {
        Task<OneTimePasswordEntity> GetActiveAsync(Guid userId);
        Task<Guid> CreateAsync(OneTimePasswordEntity oneTimePasswordEntity);
        Task DeactivateOneAsync(Guid passwordId);
        Task DeactivateAllAsync(Guid accountId);
    }
}
