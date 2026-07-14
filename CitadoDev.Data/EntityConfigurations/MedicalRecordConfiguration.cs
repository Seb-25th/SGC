using CitadoDev.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CitadoDev.Data.EntityConfigurations
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.ToTable("medical_records");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(m => m.Diagnosis).IsRequired().HasColumnType("text");
            builder.Property(m => m.Treatment).HasColumnType("text");
            builder.Property(m => m.Notes).HasColumnType("text");
            builder.Property(m => m.RecordedAt).IsRequired().HasDefaultValue(DateTime.Now);

            builder.HasOne(m => m.Appointment)
                   .WithOne(a => a.MedicalRecord)
                   .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
