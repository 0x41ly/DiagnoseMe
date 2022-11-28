namespace Core.Shared.Entities;

public class Medicine : BaseEntity{

    public string Name {get; set;} = string.Empty;
    public string Dose {get; set;} = string.Empty;
    public string Concentration {get; set;} = string.Empty;
    public string Typpe {get; set;} = string.Empty;

}