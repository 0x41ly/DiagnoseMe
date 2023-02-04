using System.Diagnostics;
using System.Security.Claims;
using Auth.Api.Common.Http;
using Auth.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Auth.Api.Controllers;


[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ApiController : ControllerBase
{
    protected readonly Serilog.ILogger _logger;
    protected ApiController(Serilog.ILogger logger)
    {
        _logger = logger;
    }
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0){
            _logger.Error("An error has been occured");
            return Problem();
        }
        if (errors.All(error => error.Type == ErrorType.Validation)){
            _logger.Error(@$"Validation errors has been occured.
                UserId: {GetUserIdFromToken()}
                Called Method: {HttpContext?.Request.Method} {HttpContext?.Request.Path}
                TraceId: {Activity.Current?.Id ?? HttpContext?.TraceIdentifier}
                Errors: [{string.Join(", ", errors.Select(error => error.Description))}]");
            return ValidationProblem(errors);
        }
        _logger.Error(@$"An error has been occured.
            UserId: {GetUserIdFromToken()}
            Called Method: {HttpContext?.Request.Method} {HttpContext?.Request.Path}
            TraceId: {Activity.Current?.Id ?? HttpContext?.TraceIdentifier}
            Errors [{string.Join(", ", errors.Select(error => error.Description))}]");
        HttpContext!.Items[HttpContextItemKeys.Errors] = errors;
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

    protected string GetUserIdFromToken()
    {
        var nameIdentifierClaims = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).ToList();
        if(nameIdentifierClaims.Count is 0)
            return "Anonymous";
        var userId = nameIdentifierClaims[1].Value;
        return userId! ?? "Anonymous";
    }
    protected List<string> GetUserRolesFromToken()
    {
        var userRoles = User.Claims.Where(claim => claim.Type.Contains("role")).Select(claim => claim.Value).ToList();
        return userRoles;
    }
}