using ErrorOr;

namespace MedicalBlog.Domain.Common.Errors;
public static partial class Errors
{
    public static class Post
    {
        public static Error NotFound => Error.Validation(
            code: "Post.NotFound",
            description: "Post not found."
        );
    }
}