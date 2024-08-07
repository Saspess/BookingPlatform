using System.Reflection;
using BP.BookingMS.Data.Contexts.Contracts;
using BP.BookingMS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BP.BookingMS.Data.Contexts
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<LandlordEntity> Landlords {  get; set; }
        public DbSet<TenantEntity> Tenants { get; set; }
        public DbSet<HotelEntity> Hotels { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
