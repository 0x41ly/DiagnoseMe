using System.Collections.Generic;

namespace Core.AuthenticationServices.Models
{
    public partial class AuthenticationResults
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<string>? Roles { get; set; }
        public bool IsConfirmed {get; set;} 
    }
}