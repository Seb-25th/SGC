using CitadoDev.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadoDev.Data.DTOs
{
    public class AppointmentDto
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
    }
}
