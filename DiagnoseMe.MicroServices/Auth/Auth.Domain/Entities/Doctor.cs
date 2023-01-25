namespace Auth.Domain.Entities;


public class Doctor : BaseEntity{
    public string Title { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string License { get; set; } = string.Empty;
    public bool IsLicenseVerified {get; set;}
    public string? ClinicId { get; set; }

    public virtual ICollection<PatientDoctor> PatientDoctors {get; set;} = new HashSet<PatientDoctor>();
    public virtual ICollection<DoctorRate> DoctorRates {get; set;} = new HashSet<DoctorRate>();
}