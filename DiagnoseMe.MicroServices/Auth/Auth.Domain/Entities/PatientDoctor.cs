namespace Auth.Domain.Entities;

public class PatientDoctor : BaseEntity{
    public string? PatientId { get; set; }
    public string? DoctorId { get; set; }
    public virtual Doctor Doctor {get; set;} = new();
    public virtual Patient Patient {get; set;} = new();
}