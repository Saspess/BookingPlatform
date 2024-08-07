using BP.BookingMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BP.BookingMS.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<HotelEntity>
    {
        public void Configure(EntityTypeBuilder<HotelEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Country)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.City)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Street)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.BuildingNumber)
                .IsRequired();

            builder.Property(c => c.LandlordId)
                .IsRequired();
        }
    }
}
