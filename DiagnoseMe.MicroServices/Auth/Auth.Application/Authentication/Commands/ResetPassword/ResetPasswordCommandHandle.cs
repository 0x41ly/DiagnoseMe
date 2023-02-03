
namespace Auth.Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandHandle :
    BaseAuthenticationHandler,
    IRequestHandler<ResetPasswordCommand, ErrorOr<AuthenticationResult>>
{

    private readonly IMemoryCache _memoryCache;
    public ResetPasswordCommandHandle(
        UserManager<ApplicationUser> userManager,
        IMemoryCache memoryCache
    ): base(userManager)
    {
        _memoryCache = memoryCache;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        if(command.Id == null)
            return Errors.User.Pin.Id.Null;
            
        var results = new AuthenticationResult();
        var jsonPin = _memoryCache.Get<string>(command.Id);
        if(jsonPin == null)
            return Errors.User.Pin.Expired;
        
        var pin = JsonConvert.DeserializeObject<Pin>(jsonPin);
        if(pin!.Type != Pins.Types.Password.Reset)
            return Errors.User.AreYouKidding;
        
        var username = pin.UserName;
        var user = await _userManager.FindByNameAsync(username!);
        var result = await _userManager.ResetPasswordAsync(
            user!,
            pin.Token!,
            command.NewPassword);
        if (!result.Succeeded)
            return Errors.User.Pin.Invalid;

        _memoryCache.Remove(command.Id);
        results.Message = "Password is successfully reset";
        return results; 
    }
}