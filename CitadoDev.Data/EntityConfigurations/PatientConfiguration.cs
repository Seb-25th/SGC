using CitadoDev.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CitadoDev.Data.EntityConfigurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Phone).HasMaxLength(20);
            builder.Property(p => p.DateOfBirth).IsRequired();
            builder.Property(p => p.IdentityDocument).IsRequired().HasMaxLength(20);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(p => p.IdentityDocument).IsUnique();
        }
    }
}
