using BP.AccountsMS.Data.Repositories.Contracts;

namespace BP.AccountsMS.Data.UnitOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        IOneTimePasswordRepository OneTimePasswordRepository { get; }
        void Commit();
    }
}
