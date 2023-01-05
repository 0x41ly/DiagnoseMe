using System.Security.Cryptography.X509Certificates;

namespace Core.Domain.Entities;

public class PatientDoctor : BaseEntity
{
    public Guid PatientId {get; set;}
    public Guid DoctorId {get; set;}
    public virtual Doctor Doctor {get; set;} = new();
    public virtual Patient Patient {get; set;} = new();

}