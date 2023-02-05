using ErrorOr;

namespace MedicalBlog.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error FailedToUnsubscribe => Error.Failure(
            code: "User.FailedToUnsubscribe",
            description: "Failed to unsubscribe."
        );
        public static Error YouAreNotSubscribedToThisDoctor => Error.Validation(
            code: "User.YouAreNotSubscribedToThisDoctor",
            description: "You are not subscribed to this doctor."
        );
        public static Error FailedToSubscribe => Error.Failure(
            code: "User.FailedToSubscribe",
            description: "Failed to subscribe."
        );
        public static Error DoctorNotFound => Error.NotFound(
            code: "User.DoctorNotFound",
            description: "Doctor not found."
        );
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