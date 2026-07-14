using CitadoDev.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadoDev.Data.DTOs
{
    public class PatientDto
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string IdentityDocument { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
