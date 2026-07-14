using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadoDev.Data.DTOs
{
    public class DoctorDto : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string LicenseNumber { get; set; }
        public int SpecialtyId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
