using CitadoDev.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CitadoDev.Data.Context
{
    public class DbContextCitadoDev : DbContext
    {
        public DbContextCitadoDev(DbContextOptions<DbContextCitadoDev> opt): base(opt) { }

        public DbSet <Appointment> Appointments { get; set; }
        public DbSet <Doctor> Doctors { get; set; }
        public DbSet <MedicalRecord> MedicalRecords { get; set; }
        public DbSet <Office> Offices { get; set; }
        public DbSet <Patient> Patients { get; set; }
        public DbSet <Specialty> Specialties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
