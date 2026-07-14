namespace CitadoDev.Data.Entities
{
    public class Patient
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string IdentityDocument { get; set; }
        public required DateTime CreatedAt { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
