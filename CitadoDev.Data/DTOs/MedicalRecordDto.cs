using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadoDev.Data.DTOs
{
    public class MedicalRecordDto
    {
        public required int Id { get; set; }
        public required int AppointmentId { get; set; }
        public required string Diagnosis { get; set; }
        public required string Treatment { get; set; }
        public string? Notes { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
