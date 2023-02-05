using ErrorOr;

namespace MedicalBlog.Domain.Common.Errors;
public static partial class Errors
{
    public static class Comment
    {
        public static Error AgreementFailed => Error.Failure(
            code: "Comment.AgreementFailed",
            description: "Comment agreement failed."
        );
        public static Error NotFound => Error.NotFound(
            code: "Comment.NotFound",
            description: "Comment not found."
        );
        public static Error AddFailed => Error.Failure(
            code: "Comment.AddFailed",
            description: "Comment Adding failed."
        );
        public static Error ParentNotFound => Error.NotFound(
            code: "Comment.ParentNotFound",
            description: "Comment parent not found."
        );

        public static Error DeletionFailed => Error.Failure(
            code: "Comment.DeletionFailed",
            description: "Comment deletion failed."
        );
        public static Error EditFailed => Error.Failure(
            code: "Comment.EditFailed",
            description: "Comment edit failed."
        );
    }
}