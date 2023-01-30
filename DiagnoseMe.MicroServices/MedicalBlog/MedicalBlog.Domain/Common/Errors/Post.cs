using ErrorOr;

namespace MedicalBlog.Domain.Common.Errors;
public static partial class Errors
{
    public static class Post
    {
        public static Error NotFound => Error.NotFound(
            code: "Post.NotFound",
            description: "Post not found."
        );
        public static Error CreationFailed => Error.Failure(
            code: "Post.CreationFailed",
            description: "Post creation failed."
        );

        public static Error YouCanNotDoThis => Error.Validation(
            code: "Post.YouCanNotDoThis",
            description: "You are not authorized to do this."
        );
        public static Error DeletionFailed => Error.Failure(
            code: "Post.DeletionFailed",
            description: "Post deletion failed."
        );
        public static Error EditFailed => Error.Failure(
            code: "Post.EditFailed",
            description: "Post edit failed."
        );
    }
}