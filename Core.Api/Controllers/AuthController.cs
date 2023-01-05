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
    [HttpPost]
    [Route("register")]
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
    [HttpPost]
    [Route("email/confirmation/resend")]
    public async Task<IActionResult> ResendEmailConfirmation(string email)
    {
        var command = new ResendEmailConfirmationCommand(email);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
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
    [HttpPost]
    [Route("password/forget")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var command = new ForgotPasswordCommand(email);        
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("password/reset")]
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
    [HttpPost]
    [Route("email/confirm")]

    public async Task<IActionResult> ConfirmEmail(string Id)
    {
        var command = new ConfirmEmailCommand(Id);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [Authorize]
    [HttpPost]
    [Route("email/change/confirm")]

    public async Task<IActionResult> ConfirmEmailChange(string newEmail, string id)
    {
        var command = new ConfirmEmailChangeCommand(newEmail,id);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet]
    [Route("signout")]
    public new async Task<IActionResult> SignOut()
    {
        var command = new SignOutCommand();
        var authResult = await _mediator.Send(command);
        return Ok();
    }

    [Authorize]
    [HttpPost]
    [Route("email/change")]
    public async Task<IActionResult> ChangeEmail(string newEmail)
    {
        var username = User.Identity!.Name;
        var command = new ChangeEmailCommand(
            username!,
            newEmail);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpPost]
    [Route("name/change")]
    public async Task<IActionResult> ChangeName(string newName)
    {
        var username = User.Identity!.Name;
        var command = new ChangeNameCommand(
            username!,
            newName);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    [Route("user/role/add")]
    public async Task<IActionResult> AddUserToRole(string username, string role)
    {
        var command = new AddUserToRoleCommand(username,role);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete]
    [Route("user/role/remove")]
    public async Task<IActionResult> RemoveUserFromRole(string username, string role)
    {
        var command = new RemoveUserFromRoleCommand(username,role);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    [Route("user/getall")]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);

    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    [Route("user/getusersinrole")]
    public async Task<IActionResult> GetUsersInRoles(string role)
    {
        var query = new GetUsersInRoleQuery(role);
        var result = await _mediator.Send(query);
        return result.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
    }

   [HttpPost]
   [AllowAnonymous]
   [Route("pin/verify")]
   public async  Task<IActionResult> VerifyPin(string pinCode)
   {
    var command = new VerifyPinCommand(pinCode);
    var result = await _mediator.Send(command);
        return result.Match(
        authResult => Ok(authResult),
        errors => Problem(errors));
   }
}
