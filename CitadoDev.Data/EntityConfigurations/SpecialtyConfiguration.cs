using CitadoDev.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CitadoDev.Data.EntityConfigurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("specialties");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).UseIdentityColumn();

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Description).HasMaxLength(300);

            builder.HasIndex(s => s.Name).IsUnique();
        }
    }
}
