using ErrorOr;
using Microsoft.AspNetCore.Identity;
namespace Core.Domain.Common.Errors;


public static partial class Errors
{
    public static partial class User
    {
        public static List<Error> MapIdentityError(List<IdentityError> errors){
            List<Error> resErrors = new List<Error>();
            foreach (var error in errors){
                resErrors.Add(Error.Validation(
                code: $"User.IdentityError.{error.Code}",
                description: error.Description));
            }
            return resErrors;
        }
    }
}