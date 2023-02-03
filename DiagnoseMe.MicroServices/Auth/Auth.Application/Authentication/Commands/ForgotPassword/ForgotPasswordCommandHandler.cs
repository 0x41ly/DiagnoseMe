


namespace Auth.Application.Authentication.Commands.ForgotPassword;


public class ForgotPasswordCommandHandler:
    BaseAuthenticationHandler,
    IRequestHandler<ForgotPasswordCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMemoryCache _memoryCache;
    private readonly ISmtp _smtp;

    public ForgotPasswordCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IMemoryCache memoryCache,
        ISmtp smtp): base(userManager)
    {
        _jwtTokenGenerator =jwtTokenGenerator;
        _memoryCache = memoryCache;
        _smtp = smtp;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var results = new AuthenticationResult();
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return Errors.User.Email.NotExist;
            
        var lastSentSince = (int) (user.LastConfirmationSentDate).Subtract(DateTime.Now).TotalSeconds;
        if(lastSentSince < 60)
            return Errors.User.Email.WaitToSend(60 - lastSentSince);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var pinCode =GenerateRandomPin();
            Pin pin = new(){
                PinCode = pinCode,
                Type = Pins.Types.Password.Reset,
                Token = token,
                UserName = user.UserName};
            string jsonPin = JsonConvert.SerializeObject(pin);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));
            _memoryCache.Set(pinCode, jsonPin, cacheEntryOptions);
        
        try
        {
            await _smtp.SendEmailAsync(
                new MailAddress(user.Email!,user.UserName),
                "Reset Password",
                $"Here Is your confirmation token: {pinCode} \n The pin code is only valid for only 1 hour"
                );
            user.LastConfirmationSentDate = DateTime.Now;
            var updateResult = await _userManager.UpdateAsync(user);
            if(!updateResult.Succeeded)
                return Errors.User.MapIdentityError(updateResult.Errors.ToList());
                
            results.Message = "Email sent successfully";
            results.Username = user.UserName;
            return results;
        }
        catch
        {
            return Errors.Smtp.SendFail;
        }
    }
}