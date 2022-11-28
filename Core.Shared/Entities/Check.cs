namespace Core.Shared.Entities;

public class Check : BaseEntity{

    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public string Report { get; set; } = string.Empty;
}