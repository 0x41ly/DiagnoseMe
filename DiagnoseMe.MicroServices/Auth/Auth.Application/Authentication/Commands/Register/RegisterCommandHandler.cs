namespace Auth.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : 
    BaseAuthenticationHandler,
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{

    private readonly ISmtp _smtp;
    private readonly IMemoryCache _memoryCache;

    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        ISmtp smtp,
        IMemoryCache memoryCache) : base(userManager)
    {
        _smtp = smtp;
        _memoryCache = memoryCache;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        command.User.DateOfBirth = DateOnly.ParseExact(command.DateOfBirth,"yyyy-MM-dd");
        var result = await _userManager.CreateAsync(command.User, command.Password);
        if(!result.Succeeded)
            return Errors.User.MapIdentityError(result.Errors.ToList());
        
        result = await _userManager.AddToRoleAsync(command.User, Roles.User);
        if(!result.Succeeded)
            return Errors.User.MapIdentityError(result.Errors.ToList());

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(command.User);
        var pinCode =GenerateRandomPin();
        Pin pin = new(){
            PinCode = pinCode,
            Type = Pins.Types.Email.Confirm,
            Token = token,
            UserName = command.User.UserName};
        string jsonPin = JsonConvert.SerializeObject(pin);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1));
        _memoryCache.Set(pinCode, jsonPin, cacheEntryOptions);
        try
        {
            await _smtp.SendEmailAsync(
                new MailAddress(command.User.Email!,command.User.UserName),
                "Email verification",
                $"Here Is your confirmation token: {pinCode} \n The pin code is only valid for only 1 hour"
                );
            return new AuthenticationResult
            {
                Message = $"We sent you an email to {command.User.Email}.\nPlease confirm your new email by entering the pin code you received.",
            };
        }
        catch
        {
            return Errors.Smtp.SendFail;
        }
    }
}