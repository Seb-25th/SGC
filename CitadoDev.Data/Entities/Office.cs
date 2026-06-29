namespace CitadoDev.Data.Entities
{
    public class Office
    {
        public required int Id { get; set; }
        public required string RoomNumber { get; set; }
        public required string Floor { get; set; }
        public required string Building { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
