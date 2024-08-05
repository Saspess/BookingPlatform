using BP.BookingMS.Data.Contexts.Contracts;
using BP.BookingMS.Data.Entities;
using BP.BookingMS.Data.Repositories;
using BP.BookingMS.Data.Repositories.Contracts;
using BP.BookingMS.Data.UnitOfWork.Contracts;

namespace BP.BookingMS.Data.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _appContext;

        private IGenericRepository<LandlordEntity> _landlordRepository;
        private IGenericRepository<TenantEntity> _tenantRepository;

        public UnitOfWork(IApplicationDbContext appContext)
        {
            _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        }

        public IGenericRepository<LandlordEntity> LandlordRepository
        {
            get
            {
                return _landlordRepository ??= new GenericRepository<LandlordEntity>(_appContext);
            }
        }

        public IGenericRepository<TenantEntity> TenantRepository
        {
            get
            {
                return _tenantRepository ??= new GenericRepository<TenantEntity>(_appContext);
            }
        }

        public async Task CommitAsync()
        {
            await _appContext.SaveChangesAsync();
        }
    }
}
