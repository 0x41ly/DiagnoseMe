namespace Core.Application.Authentication.Common;


public class Pin
{
    
    public Guid Id {get;} = Guid.NewGuid();
    public string? PinCode {get; set;}
    public string? Type {get; set;}
    public string? Token {get; set;}
    public string? UserName {get; set;}
    
}