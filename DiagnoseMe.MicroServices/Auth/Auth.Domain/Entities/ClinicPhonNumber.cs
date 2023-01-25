namespace Auth.Domain.Entities;

public class ClinicPhoneNumber : BaseEntity {
    public string? ClinicId {get; set;}
    public string PhoneNumber { get; set; } = string.Empty;
    public Clinic Clinic {get; set;}= new Clinic();

}