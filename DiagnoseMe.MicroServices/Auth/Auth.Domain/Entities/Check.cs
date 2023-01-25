namespace Auth.Domain.Entities;

public class Check : BaseEntity{

    public string? MedicalRecordId {get; set;}
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public string Report { get; set; } = string.Empty;
    
    public virtual MedicalRecord MedicalRecord {get;set;} = new MedicalRecord();
}