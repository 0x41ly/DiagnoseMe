namespace Core.Shared.Entities;

public class MedicalRecord : BaseEntity{

    public string Illness {get; set;}= string.Empty;
    public string Diagnoses {get; set;}= string.Empty;

}