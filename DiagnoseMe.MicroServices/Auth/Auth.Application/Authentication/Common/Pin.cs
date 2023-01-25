using System;
namespace Auth.Application.Authentication.Common;


public class Pin
{
    public String Id {get;} = Guid.NewGuid().ToString();
    public string? PinCode {get; set;}
    public string? Type {get; set;}
    public string? Token {get; set;}
    public string? UserName {get; set;}    
}