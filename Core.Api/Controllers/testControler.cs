using Core.Application.Settings;
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

    [HttpGet]
    [Route("get")]
    public IActionResult get(){

        return Ok();
    }

}