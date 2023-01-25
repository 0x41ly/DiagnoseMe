using ErrorOr;

namespace Auth.Domain.Common.Errors;

public static partial class Errors
{
    public static partial class User
    {
    
        public static class Credential
        {
            public static Error Invalid => Error.Validation(
                code: "User.InvalidCriedentials",
                description: "Username/Email or Password is incorrect");
        }
    }
}