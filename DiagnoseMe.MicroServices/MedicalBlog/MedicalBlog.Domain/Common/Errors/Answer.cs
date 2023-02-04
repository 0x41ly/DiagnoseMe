using ErrorOr;

namespace MedicalBlog.Domain.Common.Errors;


public static partial class Errors
{
    public static class Answer
    {
        public static Error NotFound => Error.NotFound(
            code: "Answer.NotFound",
            description: "Answer not found."
        );
        public static Error AddFailed => Error.Failure(
            code: "Answer.AddFailed",
            description: "Answer Adding failed."
        );
        public static Error ParentNotFound => Error.NotFound(
            code: "Answer.ParentNotFound",
            description: "Answer parent not found."
        );

        public static Error DeletionFailed => Error.Failure(
            code: "Answer.DeletionFailed",
            description: "Answer deletion failed."
        );
        public static Error EditFailed => Error.Failure(
            code: "Answer.EditFailed",
            description: "Answer edit failed."
        );
    }
}