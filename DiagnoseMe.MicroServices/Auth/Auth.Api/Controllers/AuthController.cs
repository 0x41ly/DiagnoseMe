using Auth.Application.Authentication.Commands.AddUserToRole;
using Auth.Application.Authentication.Commands.ChangeEmail;
using Auth.Application.Authentication.Commands.ChangeName;
using Auth.Application.Authentication.Commands.ChangePassword;
using Auth.Application.Authentication.Commands.ConfirmEmail;
using Auth.Application.Authentication.Commands.ConfirmEmailChange;
using Auth.Application.Authentication.Commands.ForgotPassword;
using Auth.Application.Authentication.Commands.Register;
using Auth.Application.Authentication.Commands.RemoveUserFromRole;
using Auth.Application.Authentication.Commands.ResendEmailConfirmation;
using Auth.Application.Authentication.Commands.ResetPassword;
using Auth.Application.Authentication.Commands.SignOut;
using Auth.Application.Authentication.Commands.UploadProfilePicture;
using Auth.Application.Authentication.Commands.VerifyPin;
using Auth.Application.Authentication.Queries.GetAllUsers;
using Auth.Application.Authentication.Queries.GetToken;
using Auth.Application.Authentication.Queries.GetUsersInRole;
using Auth.Contracts.Authentication;
using Auth.Domain.Common.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[Route("api")]
public class AuthController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public AuthController(
        ISender mediator, 
        IMapper mapper,
        Serilog.ILogger logger) : base(logger)
    {

        _mediator = mediator;
        _mapper = mapper;
    }


    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    { 
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPost("email/confirmation/resend")]
    public async Task<IActionResult> ResendEmailConfirmation(ResendEmailConfirmationRequest request)
    {
        var command = _mapper.Map<ResendEmailConfirmationCommand>(request);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> GetToken(LoginRequest request)
    {
        var query = _mapper.Map<GetTokenQuery>(request);
        var authResult = await _mediator.Send(query);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPost("password/forget")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var command = _mapper.Map<ForgotPasswordCommand>(request);        
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }
    
    [AllowAnonymous]
    [HttpPost("password/reset")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var command = _mapper.Map<ResetPasswordCommand>(request);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [Authorize]
    [HttpPost("password/change")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var command = _mapper.Map<ChangePasswordCommand>((request, User.Identity!.Name!));
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPost("email/confirm")]

    public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
    {
        var command = _mapper.Map<ConfirmEmailCommand>(request);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [Authorize]
    [HttpPost("email/change/confirm")]

    public async Task<IActionResult> ConfirmEmailChange(ConfirmEmailChangeRequest request)
    {
        var command = _mapper.Map<ConfirmEmailChangeCommand>(request);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("signout")]
    public new async Task<IActionResult> SignOut()
    {
        var command = new SignOutCommand();
        var authResult = await _mediator.Send(command);
        return Ok();
    }

    [Authorize]
    [HttpPost("email/change")]
    public async Task<IActionResult> ChangeEmail(ChangeEmailRequest request)
    {
        var command = _mapper.Map<ChangeEmailCommand>((request,User.Identity!.Name!));
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpPost("name/change")]
    public async Task<IActionResult> ChangeName(ChangeNameRequest request)
    {
        var command = _mapper.Map<ChangeNameCommand>((request, User.Identity!.Name!));
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Admin)]
    [HttpPost("user/role/{role}/add")]
    public async Task<IActionResult> AddUserToRole(AddUserToRoleRequest request, string role)
    {
        var command = _mapper.Map<AddUserToRoleCommand>((request, role));
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("user/role/{role}/remove")]
    public async Task<IActionResult> RemoveUserFromRole(RemoveUserToRoleRequest request, string role)
    {
        var command = _mapper.Map<RemoveUserFromRoleCommand>((request, role));
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("users/get/page-number/{pageNumber}")]
    public async Task<IActionResult> GetUsers(int pageNumber)
    {
        var query = new GetAllUsersQuery(pageNumber);
        var result = _mapper.Map<List<ApplicationUserResponse>>((await _mediator.Send(query)));
        return Ok(result);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("users/role/{role}/get/page-number/{pageNumber}")]
    public async Task<IActionResult> GetUsersInRoles(string role, int pageNumber)
    {
        var query = new GetUsersInRoleQuery(role, pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        authResult => Ok(_mapper.Map<List<ApplicationUserResponse>>(authResult)),
        errors => Problem(errors));
    }

   [HttpPost("pin/verify")]
   [AllowAnonymous]

   public async  Task<IActionResult> VerifyPin(VerifyPinRequest request)
   {
    var command = _mapper.Map<VerifyPinCommand>(request);
    var result = await _mediator.Send(command);
        return result.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
   }

   [HttpPost("profile/picture/upload")]
   [Authorize]
   public async  Task<IActionResult> UploadProfilePicture(UploadProfilePictureRequest request)
   {
    var command = _mapper.Map<UploadProfilePictureCommand>((request, User.Identity!.Name!)); 
    var result = await _mediator.Send(command);
        return result.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
   }   
}
