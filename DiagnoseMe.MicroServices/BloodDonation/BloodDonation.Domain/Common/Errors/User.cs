using ErrorOr;

namespace BloodDonation.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        
        public static Error SomethingWentWrong => Error.Failure(
            code: "User.SomethingWentWrong",
            description: "Something went wrong."
        );
        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User not found."
        );

        public static Error YouCanNotDoThis => Error.Validation(
            code: "Post.YouCanNotDoThis",
            description: "You are not authorized to do this."
        );
    }
}