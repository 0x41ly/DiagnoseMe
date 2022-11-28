
using System.Security.Claims;
using Core.AuthenticationServices.Authentication;
using Core.AuthenticationServices.Helpers;
using Core.AuthenticationServices.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Core.Shared.Entities;
using Core.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Core.Shared.Settings;

namespace Core.Api.Controllers;


[ApiController]
[Route("[controller]/[action]")]
[Authorize(AuthenticationSchemes = "Bearer")]
[AutoValidateAntiforgeryToken]
public class AuthController : ControllerBase
{
    private readonly IAuthentication<ApplicationUser> _auth;
    private readonly IMapper _mapper;
    private readonly Serilog.ILogger _logger;
    private readonly IServer _server;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly MailSettings _mailSettings;
    public AuthController(
        UserManager<ApplicationUser> userManager,
        IAuthentication<ApplicationUser> auth,
        IMapper mapper,
        Serilog.ILogger logger,
        IServer server,
        IConfiguration configuration
    )
    {
        _auth = auth;
        _mapper = mapper;
        _logger = logger;
        _server = server;
        _userManager = userManager;
        _mailSettings = new MailSettings{
            Mail = configuration["smtpMail"],
            Password = configuration["smtpPassword"]
        };
        Console.WriteLine(configuration.GetValue<string>("smtpMail"));

    
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(ApplicationUserDto userDto, string password)
    {
        var user = _mapper.Map<ApplicationUser>(userDto);
        var result = await _auth.RegisterAsync(user, password,_mailSettings);
        if (!result.IsSuccess)
            return BadRequest(result);
        user.LastConfirmationSentDate = DateTime.Now;
        return Ok(result);
    }

    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ResendEmailConfirmation(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if((user.LastConfirmationSentDate).Subtract(DateTime.Now).TotalMinutes > 1){
            var result = await _auth.ResendEmailConfirmationAsync(username,_mailSettings);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }
        else
        {
            var result = new AuthenticationResults
            {
                Message = "Wait atleast one minute to resend the Email confirmation."
            };
            return BadRequest(result);
        }
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> GetToken(Credentials credentials)
    {
        _logger.Information("GetToken");
        var result = await _auth.GetTokenAsync(credentials);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ForgotPassword([FromQuery] string username)
    {
        var results = await _auth.ForgotPasswordAsync(username,_mailSettings);
        
        if (!results.IsSuccess)
            return BadRequest(results);
        return Ok(results);
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ResetPassword(string username, string token, string newPassword)
    {
        var results = await _auth.ResetPasswordAsync(username, token, newPassword);
        if (!results.IsSuccess)
            return BadRequest(results);
        return Ok(results);
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string username, string token)
    {
        var results = await _auth.ConfirmEmailAsync(username, token);
        if (!results.IsSuccess)
            return BadRequest(results);
        return Ok(results);
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ConfirmEmailChange(string username, string newEmail, string token)
    {
        var results = await _auth.ConfirmEmailChangeAsync(username,newEmail ,token);
        if (!results.IsSuccess)
            return BadRequest(results);
        return Ok(results);
    }

    [Authorize]
    [HttpGet]
    public new async Task<IActionResult> SignOut()
    {
        await _auth.SignOutAsync();
        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangeEmail(string newEmail)
    {
        var username = User.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username);
        if((user.LastConfirmationSentDate).Subtract(DateTime.Now).TotalDays > 30){
            var results = await _auth.ChangeEmailAsync(username!, newEmail,_mailSettings);
            
            if (!results.IsSuccess)
                return BadRequest(results);
            return Ok(results);
        }
        else
        {
            var result = new AuthenticationResults{
                Message = "Wait atleast 30 days to change your email."
            };
            return BadRequest(result);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangeName(string newName)
    {
        var username = User.Identity!.Name;

        var results = await _auth.ChangeNameAsync(username!, newName);
        
        if (!results.IsSuccess)
            return BadRequest(results);
        return Ok(results);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> AddUserToRole([FromQuery] string username, string role)
    {
        var results = await _auth.AddUserToRoleAsync(username, role);
        if (!results.IsSuccess)
            return BadRequest(results);
        return Ok(results);
    }
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete]
    public async Task<IActionResult> RemoveUserFromRole([FromQuery] string username, string role)
    {
        _logger.Warning($"user '{User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()!.ToString().Split()[^1]}' is trying to remove user '{username}' from role '{role}'");
        var results = await _auth.RemoveUserFromRoleAsync(username, role);
        if (!results.IsSuccess)
            return BadRequest(results);
        return Ok(results);
    }
    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public IActionResult GetUsers()
    {
        var results = _auth.GetAllUsers();
        if (results == null)
            return BadRequest(results);
        return Ok(results.Select(u => _mapper.Map<ApplicationUserDto>(u)));
    }
    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetUsersInRoles([FromQuery] string role)
    {
        try
        {
            var results = await _auth.GetUsersInRoleAsync(role);
            if (results == null)
                return BadRequest(results);
            return Ok(results.Select(u => _mapper.Map<ApplicationUserDto>(u)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}