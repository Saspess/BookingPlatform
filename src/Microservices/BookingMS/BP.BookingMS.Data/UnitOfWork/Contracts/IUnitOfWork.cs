using BP.BookingMS.Data.Entities;
using BP.BookingMS.Data.Repositories.Contracts;

namespace BP.BookingMS.Data.UnitOfWork.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<LandlordEntity> LandlordRepository { get; }
        IGenericRepository<TenantEntity> TenantRepository { get; }
        Task CommitAsync();
    }
}
