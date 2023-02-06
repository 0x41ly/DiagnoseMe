using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Api.Controllers;

[Route("api")]
public class BloodDonationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public BloodDonationController(ISender mediator, 
        IMapper mapper,
        Serilog.ILogger logger) : base(logger)
    {

        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        await Task.CompletedTask;
        return Ok("test");
    }
}