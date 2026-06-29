namespace CitadoDev.Data.Entities
{
    public class MedicalRecord
    {
        public required int Id { get; set; }
        public required int AppointmentId { get; set; }
        public required string Diagnosis { get; set; }
        public required string Treatment { get; set; }
        public string? Notes { get; set; }
        public DateTime RecordedAt { get; set; }

        public Appointment? Appointment { get; set; }
    }
}
