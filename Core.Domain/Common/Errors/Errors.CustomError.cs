using ErrorOr;

namespace Core.Domain.Common.Errors;

public static partial class Errors
{

    public static Error CustomError(string description) => Error.Failure(
        code: "CustomError",
        description: description);
}