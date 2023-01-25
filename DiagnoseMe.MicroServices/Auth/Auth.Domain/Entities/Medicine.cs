namespace Auth.Domain.Entities;

public class Medicine : BaseEntity{

    public string Name {get; set;} = string.Empty;
    public string Dose {get; set;} = string.Empty;
    public string Concentration {get; set;} = string.Empty;
    public string Type {get; set;} = string.Empty;
    public string? MedicalRecordId  {get; set;}
    public MedicalRecord MedicalRecord {get; set;}

    public Medicine()
    {
        MedicalRecord = new();
    }
}