using ErrorOr;

namespace Auth.Domain.Common.Errors;

public static partial class Errors
{
    public static partial class User
    {
        public static class Name
        {
            public static Error Exists => Error.Conflict(
                code: "User.Name.Exists",
                description: "Username already exists");
            public static Error NotExists => Error.NotFound(
                code: "User.Name.NotExists",
                description: "Username does not exist");
            public static Error ChangeFail => Error.Failure(
                code: "User.Name.ChangeFail",
                description: "fialed to change your username");
            public static Error WaitToChange(int days) => Error.Validation(
                code: "User.Name.WaitToChange",
                description: $"You have to wait {days} days for next change"
            );
        }
    }
}