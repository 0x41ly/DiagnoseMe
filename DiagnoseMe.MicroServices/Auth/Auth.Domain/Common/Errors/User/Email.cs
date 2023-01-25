using ErrorOr;

namespace Auth.Domain.Common.Errors;

public static partial class Errors
{
    public static partial class User
    {
        public static class Email
        {
            public static Error AlreadyConfirmed => Error.Conflict(
                code: "User.Email.AlreadyConfirmed",
                description: "Email is already Confirmed");
            public static Error NotConfirmed => Error.Conflict(
                code: "User.Email.NotConfirmed",
                description: "Email is not Confirmed");
            public static Error ConfirmFail => Error.Failure(
                code: "User.Email.ConfirmFail",
                description: "Failed to confirm email");
            public static Error Exist => Error.Conflict(
                    code: "User.Email.Exist",
                    description: "Email already exists");
            public static Error NotExist => Error.NotFound(
                    code: "User.Email.NotExist",
                    description: "Email does not exist");
            public static Error WaitToChange(int days) => Error.Validation(
                code: "User.Email.WaitToChange",
                description: $"You have to wait {days} days for next change"
            );

            public static Error WaitToSend(int seconds) => Error.Validation(
                code: "User.Email.WaitToSend",
                description: $"You have to wait {seconds} days for next confirmation email send"
            );
        }
    }
}