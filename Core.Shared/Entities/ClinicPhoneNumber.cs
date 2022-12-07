using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace Core.Shared.Entities;

public class ClinicPhoneNumber  {
    [ForeignKey("ClinicId")]
    public Guid   ClinicId {get; set;}
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public byte[]? ConcurrencyStamp { get; set; } = null!;
    public Clinic Clinic {get; set;}= new Clinic();

}