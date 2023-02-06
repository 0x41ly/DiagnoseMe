namespace Auth.Application.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler :
    BaseAuthenticationHandler,
    IRequestHandler<ConfirmEmailCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMemoryCache _memoryCache;

    public ConfirmEmailCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IMemoryCache memoryCache): base(userManager)
    {
        _jwtTokenGenerator =jwtTokenGenerator;
        _memoryCache = memoryCache;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
    {
        if(command.Id == null)
            return Errors.User.Pin.Id.Null;

        var results = new AuthenticationResult{};
        var jsonPin = _memoryCache.Get<string>(command.Id);
        if(jsonPin == null)
            return Errors.User.Pin.Expired;

        var pin = JsonConvert.DeserializeObject<Pin>(jsonPin);
        if(pin!.Type != Pins.Types.Email.Confirm)
            return Errors.User.AreYouKidding;
        
        var username = pin.UserName;
        var user = await _userManager.FindByNameAsync(username!);
        var result = await _userManager.ConfirmEmailAsync(user!,pin.Token!);
        if (!result.Succeeded)
            return Errors.User.MapIdentityError(result.Errors.ToList());

        user!.LastEmailChangeDate = DateTime.Now;
        var updateResult = await _userManager.UpdateAsync(user);
        if(!updateResult.Succeeded)
            return Errors.User.MapIdentityError(updateResult.Errors.ToList());
            
        _memoryCache.Remove(command.Id);
        return new AuthenticationResult{
            Message = "Email is successfully confirmed",
            Username = user.UserName
        };
    }
}
