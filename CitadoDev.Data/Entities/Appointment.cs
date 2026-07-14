using CitadoDev.Data.Entities.Enums;

namespace CitadoDev.Data.Entities
{
    public class Appointment
    {
        public required int Id { get; set; }
        public required int PatientId { get; set; }
        public required int DoctorId { get; set; }
        public required int OfficeId { get; set; }
        public required DateTime ScheduledAt { get; set; }
        public required int DurationMinutes { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Office? Office { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }
    }
}
