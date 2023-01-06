using Core.Api.Common.Http;
using Core.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers;


[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        var firstError = errors[0];

        var statusCode = firstError switch
        {
            Error error when 
                error == Errors.User.Credential.Invalid ||
                error == Errors.User.AreYouKidding ||  
                error == Errors.User.Email.NotConfirmed => StatusCodes.Status403Forbidden,
            _ => firstError.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            }
        };
        return Problem(
            statusCode: statusCode,
            title: "One or more error has been occured");
    }
}