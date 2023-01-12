using Core.Application.Settings;
using Core.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Core.Api.Controllers;

[ApiController]
[Route("test")]
public class testControler : ControllerBase
{

    public testControler()
    {}

    [HttpPost]
    [Route("get")]
    public IActionResult get(LoginRequest request){

        return Ok(DateOnly.ParseExact("31 Dec 2000", "dd MMM yyyy").ToString());
    }

}