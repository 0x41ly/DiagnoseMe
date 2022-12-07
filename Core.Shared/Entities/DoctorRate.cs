using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace Core.Shared.Entities;

public class DoctorRate : BaseEntity{
    public DoctorRate(){
        Doctor = new Doctor();
        Patient = new Patient();
    }

    public int Rate {get; set;}
    public Guid PatientId {get; set;}
    public Guid DoctorId {get; set;}
    public virtual Doctor Doctor {get; set;}
    public virtual Patient Patient {get; set;}
}