using BP.AccountsMS.Data.Repositories.Contracts;

namespace BP.AccountsMS.Data.UnitOfWork.Contracts
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        void Commit();
    }
}
