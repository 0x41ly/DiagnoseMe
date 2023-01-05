using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace Core.Domain.Entities;

public class ClinicPhoneNumber : BaseEntity {
    [ForeignKey("ClinicId")]
    public Guid   ClinicId {get; set;}
    public string PhoneNumber { get; set; } = string.Empty;
    public Clinic Clinic {get; set;}= new Clinic();

}