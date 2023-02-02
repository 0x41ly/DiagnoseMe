using ErrorOr;

namespace MedicalBlog.Domain.Common.Errors;

public static partial class Errors
{
    public static class Question
    {
        public static Error NotFound => Error.NotFound(
            code: "Question.NotFound",
            description: "Question not found."
        );
        public static Error CreationFailed => Error.Failure(
            code: "Question.CreationFailed",
            description: "Question creation failed."
        );

        public static Error DeletionFailed => Error.Failure(
            code: "Question.DeletionFailed",
            description: "Question deletion failed."
        );
        public static Error EditFailed => Error.Failure(
            code: "Question.EditFailed",
            description: "Question edit failed."
        );
    }
}