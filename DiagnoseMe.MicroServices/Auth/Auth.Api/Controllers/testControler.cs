using Auth.Application.Settings;
using Auth.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Auth.Api.Controllers;

[ApiController]
[Route("test")]
public class testControler : ControllerBase
{

    public testControler()
    {}

    [HttpPost]
    [Route("get")]
    public IActionResult get(LoginRequest request){

        return Ok(DateTime.Now.ToString());
    }

}