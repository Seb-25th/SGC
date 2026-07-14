
namespace CitadoDev.Data.DTOs
{
    public class OfficeDto : BaseEntity<int>
    {
        public required string RoomNumber { get; set; }
        public required string Floor { get; set; }
        public required string Building { get; set; }
    }
}
