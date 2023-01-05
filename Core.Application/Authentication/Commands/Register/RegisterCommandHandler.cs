using Core.Domain.Common;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Core.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : 
    BaseHandler,
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResults>>
{

    private readonly ISmtp _smtp;
    private readonly IMemoryCache _memoryCache;
    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        ISmtp smtp,
        IMemoryCache memoryCache
    ): base(userManager)
    {
        _smtp = smtp;
        _memoryCache = memoryCache;
    }
    public async Task<ErrorOr<AuthenticationResults>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        ApplicationUser user = new(){
            FirstName = command.FirstName,
            LastName = command.LastName,
            UserName = command.UserName,
            Email = command.Email
        };

        if (await _userManager.FindByEmailAsync(user.Email) != null)
            return Errors.User.Email.Exist;
        if (await _userManager.FindByNameAsync(user.UserName) != null)
            return Errors.User.Name.Exist;
            
        var result = await _userManager.CreateAsync(user, command.Password);
        var errorMessages = result.Errors.Select(e => e.Description);
        if (!result.Succeeded)
            return Errors.CustomError(String.Join(" , ", errorMessages));
        // await _userManager.AddToRoleAsync(user, Roles.User);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var pinCode =GenerateRandomPin();
        Pin pin = new(){
            PinCode = pinCode,
            Type = Pins.Types.Email.Confirm,
            Token = token,
            UserName = user.UserName};
        string jsonPin = JsonConvert.SerializeObject(pin);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1));
        _memoryCache.Set(pinCode, jsonPin, cacheEntryOptions);
        try
        {
            await _smtp.SendEmailAsync(
                new MailAddress(user.Email,user.UserName),
                "Email verification",
                $"Here Is your confirmation token: {pinCode} \n The pin code is only valid for only 1 hour"
                );
            return new AuthenticationResults
            {
                Message = $"We sent you an email to {user.Email}.\nPlease confirm your new email by entering the pin code you received.",
            };
        }
        catch
        {
            return Errors.Smtp.SendFail;
        }
    }
}