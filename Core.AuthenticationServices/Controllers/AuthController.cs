using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.AuthenticationServices.Authentication;
using Core.AuthenticationServices.Helpers;
using Core.AuthenticationServices.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Core.AuthenticationServices.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public abstract class AuthController<TUser, TUserDto> : ControllerBase
where TUser : IdentityUser
{
    private readonly IAuthentication<TUser> _auth;
    private readonly IMapper _mapper;
    private readonly Serilog.ILogger _logger;
    private readonly IServer _server;
    public AuthController(IAuthentication<TUser> auth, IMapper mapper, Serilog.ILogger logger, IServer server)
    {
        _auth = auth;
        _mapper = mapper;
        _logger = logger;
        _server = server;
        
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(TUserDto userDto, string password)
    {
        var user = _mapper.Map<TUser>(userDto);
        var result = await _auth.RegisterAsync(user, password, GetDomainName());
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
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
        var results = await _auth.ForgotPasswordAsync(username, GetDomainName());
        
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
    public async Task<IActionResult> SignOut()
    {
        await _auth.SignOutAsync();
        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangeEmail(string newEmail)
    {
        var username = User.Identity.Name;

        var results = await _auth.ChangeEmailAsync(username, newEmail, GetDomainName());
        
        if (!results.IsSuccess)
            return BadRequest(results);
        return Ok(results);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangeName(string newName)
    {
        var username = User.Identity.Name;

        var results = await _auth.ChangeNameAsync(username, newName);
        
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
        return Ok(results.Select(u => _mapper.Map<TUserDto>(u)));
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
            return Ok(results.Select(u => _mapper.Map<TUserDto>(u)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private string GetDomainName()
    {
        string domainName = "";
        string jsonText = System.IO.File.ReadAllText("Properties/launchSettings.json");  
        dynamic data = JObject.Parse(jsonText);
        try{        
            domainName = data.DomainName; 
        }
        catch
        {
            var addresses = _server.Features.Get<IServerAddressesFeature>().Addresses;
            domainName = string.Join(", ", addresses).Split(',').ToList()[0];
        }
        return domainName;
        
    }
}