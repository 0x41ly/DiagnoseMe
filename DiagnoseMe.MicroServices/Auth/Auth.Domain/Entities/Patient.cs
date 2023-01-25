namespace Auth.Domain.Entities;

public class Patient : BaseEntity{
    public float Height {get; set;}
    public float Weight {get; set;}

    public virtual ICollection<PatientDoctor> PatientDoctors {get; set;} = new HashSet<PatientDoctor>();
    public virtual ICollection<DoctorRate> DoctorRates {get; set;} = new HashSet<DoctorRate>();
}