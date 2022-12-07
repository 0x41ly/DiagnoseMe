
namespace Core.Shared.Entities;

public class Patient : BaseEntity{
    public Patient()
        {
        Doctors = new HashSet<Doctor>();
        User = new();
        DoctorRates = new HashSet<DoctorRate>();
        MedicalRecords = new HashSet<MedicalRecord>();
        PatientDoctors = new HashSet<PatientDoctor>();
    }
    public string UserId {get; set;} = string.Empty;
    public float Height {get; set;}
    public float Weight {get; set;}
    public virtual ICollection<Doctor> Doctors {get; set;}
    public virtual ApplicationUser User {get; set;}
    public virtual ICollection<DoctorRate> DoctorRates {get; set;}
    public virtual ICollection<MedicalRecord> MedicalRecords {get; set;}
    public virtual ICollection<PatientDoctor> PatientDoctors {get; set;}



}