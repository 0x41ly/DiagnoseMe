using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;

namespace Core.Shared.Entities;

public class Chat : BaseEntity{

    public string SenderId { get; set; } = string.Empty;
    public string RecieverId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsDelivered { get; set; } 
    public bool IsSeen { get; set; } 
    public string MessageType { get; set; } = string.Empty;

    public virtual ApplicationUser  Sender {get; set;} = new ApplicationUser();
     public virtual ApplicationUser  Reciever {get; set;} = new ApplicationUser();

}