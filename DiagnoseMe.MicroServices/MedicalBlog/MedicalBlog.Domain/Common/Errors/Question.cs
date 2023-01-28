using ErrorOr;

namespace MedicalBlog.Domain.Common.Errors;

public static partial class Errors
{
    public static class Question
    {
        public static Error NotFound => Error.Validation(
            code: "Question.NotFound",
            description: "Question not found."
        );
    }
}