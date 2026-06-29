using CitadoDev.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CitadoDev.Data.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("appointments");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).UseIdentityColumn();

            builder.Property(a => a.ScheduledAt).IsRequired();
            builder.Property(a => a.DurationMinutes).IsRequired().HasDefaultValue(30);
            builder.Property(a => a.Status).IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20);
            builder.Property(a => a.Reason).HasMaxLength(300);
            builder.Property(a => a.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);

            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appointments)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Office)
                   .WithMany(o => o.Appointments)
                   .HasForeignKey(a => a.OfficeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
