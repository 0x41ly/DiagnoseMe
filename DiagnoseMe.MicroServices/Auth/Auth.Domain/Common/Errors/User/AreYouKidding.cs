using ErrorOr;
namespace Auth.Domain.Common.Errors;

public static partial class Errors
{
    public static partial class User
    {

        public static Error AreYouKidding => Error.Validation(
            code: "User.AreYouKidding",
            description: "Are you kidding me lol?.");
    }
}