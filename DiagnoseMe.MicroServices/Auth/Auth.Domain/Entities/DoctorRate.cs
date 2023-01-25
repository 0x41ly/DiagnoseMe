namespace Auth.Domain.Entities;

public class DoctorRate : BaseEntity{
    public DoctorRate(){
        Doctor = new Doctor();
        Patient = new Patient();
    }

    public int Rate {get; set;}
    public string? PatientId {get; set;}
    public string? DoctorId {get; set;}
    public virtual Doctor Doctor {get; set;}
    public virtual Patient Patient {get; set;}
}