using System;

public class Appointment
{
    public int Id { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = "Scheduled";

    public string Reason { get; set; } = string.Empty;

    public int PatientId { get; set; }

    public Patient Patient { get; set; } = null!;

    public int ProfessionalId { get; set; }

    public Professional Professional { get; set; } = null!;
}
