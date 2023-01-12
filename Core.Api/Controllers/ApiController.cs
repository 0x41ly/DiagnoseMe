using Core.Api.Common.Http;
using Core.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Core.Api.Controllers;


[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
            return Problem();
        if (errors.All(error => error.Type == ErrorType.Validation))
            return ValidationProblem(errors);
        
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        return Problem(errors[0]);
    }

    private IActionResult Problem(Error firstError)
    {
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
            title: "An error has been occured");
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
              error.Code,
              error.Description);
        }
        return ValidationProblem(modelStateDictionary);
    }
}