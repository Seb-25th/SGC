using CitadoDev.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CitadoDev.Data.EntityConfigurations
{
    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable("offices");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseIdentityColumn();

            builder.Property(o => o.RoomNumber).IsRequired().HasMaxLength(10);
            builder.Property(o => o.Floor).HasMaxLength(10);
            builder.Property(o => o.Building).HasMaxLength(100);
        }
    }
}
