using ErrorOr;
using Microsoft.AspNetCore.Identity;
namespace Auth.Domain.Common.Errors;


public static partial class Errors
{
    public static partial class User
    {
        public static List<Error> MapIdentityError(List<IdentityError> errors){
            
            var resErrors = errors.ConvertAll(
                error => Error.Validation(
                    error.Code, error.Description));
            return resErrors;
        }
    }
}