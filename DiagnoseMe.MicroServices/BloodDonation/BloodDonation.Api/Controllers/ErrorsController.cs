using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Api.Controllers;

[ApiController]
[Route("/error")]
public class ErrorsController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error; 

        var (statusCode, message) = exception switch
        {
            _ => (StatusCodes.Status500InternalServerError ,"An unexpected error occurred")
        };
        return Problem(
            statusCode: statusCode,
            title: message
            );
    }    
}