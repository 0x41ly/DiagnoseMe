namespace Core.Application.Authentication.Commands.ChangeEmail;

public class ChangeEmailCommandHandeler :
    BaseHandler,
    IRequestHandler<ChangeEmailCommand, ErrorOr<AuthenticationResults>>
{
    private readonly ISmtp _smtp;
    private readonly IMemoryCache _memoryCache;
    public ChangeEmailCommandHandeler(
        UserManager<ApplicationUser> userManager,
        ISmtp smtp,
        IMemoryCache memoryCache
    ): base(userManager)
    {
        _smtp = smtp;
        _memoryCache = memoryCache;
    }

    public async Task<ErrorOr<AuthenticationResults>> Handle(ChangeEmailCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(command.UserName);
        var confirmedSince = (int) (user.LastConfirmationSentDate).Subtract(DateTime.Now).TotalDays;
        if(confirmedSince < 30)
            return Errors.User.Email.WaitToChange(30 - confirmedSince);
        if(!user.EmailConfirmed)
            return Errors.User.Email.NotConfirmed;
        
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, command.NewEmail);
        var pinCode =GenerateRandomPin();
        Pin pin = new(){
            PinCode = pinCode,
            Type = Pins.Types.Email.Change,
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
                "Change Email",
                $"Here Is your confirmation token: {pinCode} \n The pin code is only valid for only 1 hour");
            return new AuthenticationResults
            {
                Message = $"We sent you an email to {user.Email}.\nPlease confirm your new email by entering the pin code you received.",
                Username = user.UserName
            };
        }
        catch
        {
            return Errors.Smtp.SendFail;
        }
        
    }
}