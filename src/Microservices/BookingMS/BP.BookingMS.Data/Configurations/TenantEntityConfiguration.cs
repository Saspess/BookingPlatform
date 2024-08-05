using BP.BookingMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BP.BookingMS.Data.Configurations
{
    public class TenantEntityConfiguration : IEntityTypeConfiguration<TenantEntity>
    {
        public void Configure(EntityTypeBuilder<TenantEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.Email)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
