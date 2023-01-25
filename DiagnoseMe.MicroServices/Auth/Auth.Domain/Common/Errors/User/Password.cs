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
        }
    }
}