
namespace Core.Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandHandle :
    BaseHandler,
    IRequestHandler<ResetPasswordCommand, ErrorOr<AuthenticationResults>>
{

    private readonly IMemoryCache _memoryCache;
    public ResetPasswordCommandHandle(
        UserManager<ApplicationUser> userManager,
        IMemoryCache memoryCache
    ): base(userManager)
    {
        _memoryCache = memoryCache;
    }

    public async Task<ErrorOr<AuthenticationResults>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        if(command.Id == null)
            return Errors.User.Pin.Id.Null;
            
        var results = new AuthenticationResults();
        var jsonPin = _memoryCache.Get<string>(command.Id);
        if(jsonPin == null)
            return Errors.User.Pin.Expired;
        
        var pin = JsonConvert.DeserializeObject<Pin>(jsonPin);
        if(pin.Type != Pins.Types.Password.Reset)
            return Errors.User.AreYouKidding;
        
        var username = pin.UserName;
        var user = await _userManager.FindByNameAsync(username);
        var result = await _userManager.ResetPasswordAsync(
            user,
            pin.Token,
            command.NewPassword);
        if (!result.Succeeded)
            return Errors.User.Pin.Invalid;
        
        results.Message = "Password reset successfully";
        return results; 
    }
}