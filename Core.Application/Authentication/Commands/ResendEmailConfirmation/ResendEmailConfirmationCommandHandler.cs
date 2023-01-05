


namespace Core.Application.Authentication.Commands.ResendEmailConfirmation;

public class ResendEmailConfirmationCommandHandler:
    BaseHandler,
    IRequestHandler<ResendEmailConfirmationCommand, ErrorOr<AuthenticationResults>>
{

    private readonly ISmtp _smtp;
    private readonly IMemoryCache _memoryCache;
    public ResendEmailConfirmationCommandHandler(
        UserManager<ApplicationUser> userManager,
        ISmtp smtp,
        IMemoryCache memoryCache
    ): base(userManager)
    {
        _smtp = smtp;
        _memoryCache = memoryCache;
    }

    public async Task<ErrorOr<AuthenticationResults>> Handle(ResendEmailConfirmationCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if(user.EmailConfirmed)
            return Errors.User.Email.AlreadyConfirmed;

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