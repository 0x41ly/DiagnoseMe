using ErrorOr;

namespace Auth.Domain.Common.Errors;

public static partial class Errors
{

    public static Error UnExpected => Error.Failure(
        code: "UnExpected",
        description: " error occured");
}