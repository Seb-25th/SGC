public class Patient : User
{
    public DateTime DateOfBirth { get; set; }

    public string Address { get; set; } = string.Empty;

    public string EmergencyContact { get; set; } = string.Empty;

    public string MedicalConditions { get; set; } = string.Empty;

    public string InsurancePolicyNumber { get; set; } = string.Empty;

    public Guid InsuranceProviderId { get; set; }

    public InsuranceProvider InsuranceProvider { get; set; } = null!;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}