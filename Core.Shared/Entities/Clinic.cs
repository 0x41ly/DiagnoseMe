namespace Core.Shared.Entities;

public class Clinic : BaseEntity{

    public string Name { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}