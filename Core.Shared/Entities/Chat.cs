namespace Core.Shared.Entities;

public class Chat : BaseEntity{

    public string Message { get; set; } = string.Empty;
    public bool IsDelivered { get; set; } 
    public bool IsSeen { get; set; } 
    public string MessageType { get; set; } = string.Empty;

}