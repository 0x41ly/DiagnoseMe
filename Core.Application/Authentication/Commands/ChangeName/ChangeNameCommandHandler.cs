namespace Core.Application.Authentication.Commands.ChangeName;

public class ChangeNameCommandHandler :
    BaseHandler,
    IRequestHandler<ChangeNameCommand, ErrorOr<AuthenticationResults>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public ChangeNameCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator
    ): base(userManager)
    {
        _jwtTokenGenerator =jwtTokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResults>> Handle(ChangeNameCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(command.UserName);

        if(!user.EmailConfirmed)
            return Errors.User.Email.NotConfirmed;
        
        if (await _userManager.FindByNameAsync(command.NewUserName) != null)
            return Errors.User.Name.Exist;

        var setUserNameResult = await _userManager.SetUserNameAsync(user, command.NewUserName);
        if (!setUserNameResult.Succeeded)
            return Errors.User.Name.ChangeFail;
            
        return new AuthenticationResults
        {
            Message = "Successfully changed your username",
            Token = new JwtSecurityTokenHandler().WriteToken(_jwtTokenGenerator.GenerateJwtTokenAsync(
                user.Id,
                user.UserName,
                await GetUserClaims(user))),
            Username = user.UserName
        };
        
    }
}