namespace MedicalBlog.Domain.Entities;

public class Booking : BaseEntity{

    public DateTime Date {get; set;}
    public string? DoctorId {get; set;}
    public string? PatientId {get; set;}
      
}