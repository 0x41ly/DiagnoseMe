using System.Security.Cryptography.X509Certificates;

namespace Core.Shared.Entities;

public class MedicalRecord : BaseEntity{
    public MedicalRecord(){
        Doctor = new();
        Patient = new();
        
        Medicines = new HashSet<Medicine>();
        Checks = new HashSet<Check>();

    }

    public string Illness {get; set;}= string.Empty;
    public string Diagnoses {get; set;}= string.Empty;
    public Guid PatientId  {get; set;}
    public Guid DoctorId {get; set;}
    public virtual Doctor Doctor {get; set;}
    public virtual Patient Patient { get; set;}
    public virtual ICollection<Medicine> Medicines {get; set;}
    public virtual ICollection<Check> Checks {get; set;}

}