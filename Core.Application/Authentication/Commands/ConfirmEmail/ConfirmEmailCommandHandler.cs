namespace Core.Application.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler :
    BaseHandler,
    IRequestHandler<ConfirmEmailCommand, ErrorOr<AuthenticationResults>>
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

    public async Task<ErrorOr<AuthenticationResults>> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
    {
        if(command.Id == null)
            return Errors.User.Pin.Id.Null;

        var results = new AuthenticationResults{};
        var jsonPin = _memoryCache.Get<string>(command.Id);
        if(jsonPin == null)
            return Errors.User.Pin.Expired;

        var pin = JsonConvert.DeserializeObject<Pin>(jsonPin);
        if(pin.Type != Pins.Types.Email.Confirm)
            return Errors.User.AreYouKidding;
        
        var username = pin.UserName;
        var user = await _userManager.FindByNameAsync(username);
        var result = await _userManager.ConfirmEmailAsync(user,pin.Token);
        if (!result.Succeeded)
            return Errors.User.Pin.Invalid;

        _memoryCache.Remove(command.Id);
        return new AuthenticationResults{
            Message = "Email confirmed successfully",
            Token =  new JwtSecurityTokenHandler().WriteToken(_jwtTokenGenerator.GenerateJwtTokenAsync(
                user.Id,
                user.UserName,
                await GetUserClaims(user))),
            Username = user.UserName
        };
    }
}
