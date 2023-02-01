using ErrorOr;

namespace Auth.Domain.Common.Errors;

public static partial class Errors
{
    public static partial class User
    {
        public static class Password
        {
            public static Error ResetFail => Error.Failure(
                code: "User.password.ResetFail",
                description: "Failed to rest password");
            
            public static Error Invalid => Error.Validation(
                code: "User.password.Invalid",
                description: "Invalid password");
            
            public static Error ChangeFail => Error.Failure(
                code: "User.password.ChangeFail",
                description: "Failed to change password");
        }
    }
}