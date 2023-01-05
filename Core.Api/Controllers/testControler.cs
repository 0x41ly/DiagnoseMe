using Core.Application.Settings;
using Core.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Core.Api.Controllers;

[ApiController]
[Route("test")]
public class testControler : ControllerBase
{
    private readonly MailSettings _mailSettings;

    public testControler(
        IOptions<MailSettings> mailSettings
    )
    {
        _mailSettings = mailSettings.Value;
    }

    [HttpPost]
    [Route("get")]
    public IActionResult get(LoginRequest request){

        return Ok(request);
    }

}