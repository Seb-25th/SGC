namespace CitadoDev.Data.Entities
{
    public class Doctor
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string LicenseNumber { get; set; }
        public int SpecialtyId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Specialty? Specialty { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
