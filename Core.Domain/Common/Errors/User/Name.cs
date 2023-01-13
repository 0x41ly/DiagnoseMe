using ErrorOr;

namespace Core.Domain.Common.Errors;

public static partial class Errors
{
    public static partial class User
    {
        public static class Name
        {
            public static Error Exist => Error.Conflict(
                code: "User.Name.Exist",
                description: "Username already exists");
            public static Error NotExist => Error.NotFound(
                code: "User.Name.NotExist",
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