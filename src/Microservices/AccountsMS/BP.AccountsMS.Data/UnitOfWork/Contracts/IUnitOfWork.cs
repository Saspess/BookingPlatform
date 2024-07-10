using BP.AccountsMS.Data.Repositories.Contracts;

namespace BP.AccountsMS.Data.UnitOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        void Commit();
    }
}
