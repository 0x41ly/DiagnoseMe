namespace Auth.Domain.Entities;

public class Clinic : BaseEntity{
    public Clinic(){
        ClinicPhoneNumbers = new HashSet<ClinicPhoneNumber>();
        Doctors = new HashSet<Doctor>();
    }
    public string Name { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public virtual ICollection<ClinicPhoneNumber> ClinicPhoneNumbers {get; set;} 
    public virtual ICollection<Doctor> Doctors {get; set;} 

}