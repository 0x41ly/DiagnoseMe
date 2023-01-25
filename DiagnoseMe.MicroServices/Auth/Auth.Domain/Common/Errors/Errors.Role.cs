using ErrorOr;

namespace Auth.Domain.Common.Errors;


public static partial class Errors
{
    public static class Role
    {
        public static Error RoleNotExist => Error.NotFound(
            code: "Role.RoleNotExist",
            description: "Role does not exist");
        public static Error FialToAdd => Error.Failure(
            code: "Role.FialToAdd",
            description: "Failed to add user to role");
        public static Error FialToRemove => Error.Failure(
            code: "Role.FialToAdd",
            description: "Failed to remove user from role");
    }
}