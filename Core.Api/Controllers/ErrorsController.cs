using Core.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers;

[ApiController]
[Route("/error")]
public class ErrorsController : ControllerBase
{

    [HttpGet]    
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error; 

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => ((int) serviceException.statusCode, serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError ,"An unexpected error occurred")
        };
        return Problem(
            statusCode: statusCode,
            title: message
            );
    }    
}