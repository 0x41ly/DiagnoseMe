using System;
namespace Core.Domain.Entities;

public class Booking : BaseEntity{

    public DateTime Date {get; set;}
    public Guid DoctorId {get; set;}
    public virtual Doctor Doctor  {get; set;} = new();

    public Guid PatientId {get; set;}
    public virtual Patient Patient  {get; set;} = new();
      
}