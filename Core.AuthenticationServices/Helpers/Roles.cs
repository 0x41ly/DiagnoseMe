
using System.Collections.Generic;

namespace Core.AuthenticationServices.Helpers
{
    public static partial class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public static List<string> AvailableRoles = new List<string>() { Admin, User };
    }
}
