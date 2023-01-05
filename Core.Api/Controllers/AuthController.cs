using Core.Application.Authentication.Commands.AddUserToRole;
using Core.Application.Authentication.Commands.ChangeEmail;
using Core.Application.Authentication.Commands.ChangeName;
using Core.Application.Authentication.Commands.ConfirmEmail;
using Core.Application.Authentication.Commands.ConfirmEmailChange;
using Core.Application.Authentication.Commands.ForgotPassword;
using Core.Application.Authentication.Commands.Register;
using Core.Application.Authentication.Commands.RemoveUserFromRole;
using Core.Application.Authentication.Commands.ResendEmailConfirmation;
using Core.Application.Authentication.Commands.ResetPassword;
using Core.Application.Authentication.Commands.SignOut;
using Core.Application.Authentication.Commands.VerifyPin;
using Core.Application.Authentication.Queries.GetAllUsers;
using Core.Application.Authentication.Queries.GetToken;
using Core.Application.Authentication.Queries.GetUsersInRole;
using Core.Contracts.Authentication;
using Core.Domain.Common.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers;

[Route("auth")]
public class AuthController : ApiController
{
    private readonly ISender _mediator;

    public AuthController(ISender  mediator)
    {

        _mediator = mediator;
    }


    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.UserName,
            request.Email,
            request.Password);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPost("email/confirmation/resend")]
    public async Task<IActionResult> ResendEmailConfirmation(ResendEmailConfirmationRequest request)
    {
        var command = new ResendEmailConfirmationCommand(request.Email);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> GetToken(LoginRequest request)
    {
        var query = new GetTokenQuery(
            request.Email,
            request.Password);
        var authResult = await _mediator.Send(query);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPost("password/forget")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var command = new ForgotPasswordCommand(request.Email);        
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }
    
    [AllowAnonymous]
    [HttpPost("password/reset")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var command = new ResetPasswordCommand(
            request.Id,
            request.NewPassword);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }
    [AllowAnonymous]
    [HttpPost("email/confirm")]

    public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
    {
        var command = new ConfirmEmailCommand(request.Id);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [Authorize]
    [HttpPost("email/change/confirm")]

    public async Task<IActionResult> ConfirmEmailChange(ConfirmEmailChangeRequest request)
    {
        var command = new ConfirmEmailChangeCommand(
            request.NewEmail,
            request.Id);
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
        var username = User.Identity!.Name;
        var command = new ChangeEmailCommand(
            username!,
            request.NewEmail);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpPost("name/change")]
    public async Task<IActionResult> ChangeName(ChangeNameRequest request)
    {
        var username = User.Identity!.Name;
        var command = new ChangeNameCommand(
            username!,
            request.NewName);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Admin)]
    [HttpPost("user/{role}/add")]
    public async Task<IActionResult> AddUserToRole(AddUserToRoleRequest request, string role)
    {
        var command = new AddUserToRoleCommand(
            request.UserName,
            role);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("user/{role}/remove")]
    public async Task<IActionResult> RemoveUserFromRole(RemoveUserToRoleRequest request, string role)
    {
        var command = new RemoveUserFromRoleCommand(
            request.UserName,
            role);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("users/get")]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);

    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("users/{role}/get")]
    public async Task<IActionResult> GetUsersInRoles(string role)
    {
        var query = new GetUsersInRoleQuery(role);
        var result = await _mediator.Send(query);
        return result.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }

   [HttpPost("pin/verify")]
   [AllowAnonymous]

   public async  Task<IActionResult> VerifyPin(VerifyPinRequest request)
   {
    var command = new VerifyPinCommand(request.PinCode);
    var result = await _mediator.Send(command);
        return result.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
   }
}
