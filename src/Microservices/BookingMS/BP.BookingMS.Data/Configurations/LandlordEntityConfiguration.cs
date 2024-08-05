using BP.BookingMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BP.BookingMS.Data.Configurations
{
    public class LandlordEntityConfiguratoin : IEntityTypeConfiguration<LandlordEntity>
    {
        public void Configure(EntityTypeBuilder<LandlordEntity> builder)
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
