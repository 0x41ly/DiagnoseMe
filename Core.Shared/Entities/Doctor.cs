namespace Core.Shared.Entities;

public class Doctor : BaseEntity{

    public string Title { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string License { get; set; } = string.Empty;

}