using CitadoDev.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CitadoDev.Data.EntityConfigurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("doctors");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).UseIdentityColumn();

            builder.Property(d => d.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.LastName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Email).IsRequired().HasMaxLength(150);
            builder.Property(d => d.LicenseNumber).IsRequired().HasMaxLength(50);
            builder.Property(d => d.SpecialtyId).IsRequired();
            builder.Property(d => d.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);

            builder.HasIndex(d => d.Email).IsUnique();
            builder.HasIndex(d => d.LicenseNumber).IsUnique();

            builder.HasOne(d => d.Specialty)
                   .WithMany(s => s.Doctors)
                   .HasForeignKey(d => d.SpecialtyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
