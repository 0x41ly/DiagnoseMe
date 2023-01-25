using ErrorOr;

namespace Auth.Domain.Common.Errors;

public static partial class Errors
{
    public static partial class User
    {
        public static class File
        {
            public static Error NotB64 => Error.Validation(
                code: "User.File.NotB64",
                description: "File Should be base64 Encoded");
            
            public static Error NotImage => Error.Validation(
                code: "User.File.NotImage",
                description: "Only Images are allowed");

        }
    }
}