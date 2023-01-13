

namespace Core.Application.Authentication.Commands.ConfirmEmailChange;

public class ConfirmEmailChangeCommandHandler :
    BaseAuthenticationHandler,
    IRequestHandler<ConfirmEmailChangeCommand, ErrorOr<AuthenticationResults>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMemoryCache _memoryCache;

    public ConfirmEmailChangeCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IMemoryCache memoryCache): base(userManager)
    {
        _jwtTokenGenerator =jwtTokenGenerator;
        _memoryCache = memoryCache;
    }

    public async Task<ErrorOr<AuthenticationResults>> Handle(ConfirmEmailChangeCommand command, CancellationToken cancellationToken)
    {
        if(command.Id == null)
            return Errors.User.Pin.Id.Null;
            
        var results = new AuthenticationResults{};
        var jsonPin = _memoryCache.Get<string>(command.Id);
        if(jsonPin == null)
            return Errors.User.Pin.Expired;
        
        var pin = JsonConvert.DeserializeObject<Pin>(jsonPin);
        if(pin!.Type != Pins.Types.Email.Change)
            return Errors.User.AreYouKidding;
        var username = pin.UserName;
        var user = await _userManager.FindByNameAsync(username);
        var result = await _userManager.ChangeEmailAsync(user, command.NewEmail, pin.Token);
        if (!result.Succeeded)
            return Errors.User.Pin.Invalid;
        
        user.LastEmailChangeDate = DateTime.Now;
        var updateResult = await _userManager.UpdateAsync(user);
        if(!updateResult.Succeeded)
            return Errors.User.MapIdentityError(updateResult.Errors.ToList());
            
        _memoryCache.Remove(command.Id);
        return new AuthenticationResults{
            Message = "Email is successfully confirmed",
            Token =  new JwtSecurityTokenHandler().WriteToken(_jwtTokenGenerator.GenerateJwtTokenAsync(
                user.Id,
                user.UserName,
                await GetUserClaims(user))),
            Username = user.UserName,
        };
    }
}