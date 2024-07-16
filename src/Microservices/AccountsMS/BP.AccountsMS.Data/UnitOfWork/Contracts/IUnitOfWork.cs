using BP.AccountsMS.Data.Repositories.Contracts;

namespace BP.AccountsMS.Data.UnitOfWork.Contracts
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        void Commit();
    }
}
