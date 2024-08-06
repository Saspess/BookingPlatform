using BP.BookingMS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BP.BookingMS.Data.Contexts.Contracts
{
    internal interface IApplicationDbContext
    {
        DbSet<LandlordEntity> Landlords { get; set; }
        DbSet<TenantEntity> Tenants { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
