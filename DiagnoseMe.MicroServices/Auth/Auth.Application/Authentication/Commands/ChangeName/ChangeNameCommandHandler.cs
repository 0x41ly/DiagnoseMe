namespace Auth.Application.Authentication.Commands.ChangeName;

public class ChangeNameCommandHandler :
    BaseAuthenticationHandler,
    IRequestHandler<ChangeNameCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public ChangeNameCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator
    ): base(userManager)
    {
        _jwtTokenGenerator =jwtTokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(ChangeNameCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(command.UserName);

        if(!user!.EmailConfirmed)
            return Errors.User.Email.NotConfirmed;

        var lastChanges = (int) user.LastUserNameChangeDate.Subtract(DateTime.Now).TotalDays;
        if(lastChanges < 30)
            return Errors.User.Name.WaitToChange(30 - lastChanges);
        
        if (await _userManager.FindByNameAsync(command.NewUserName) != null)
            return Errors.User.Name.Exists;

        var result = await _userManager.SetUserNameAsync(user, command.NewUserName);
        if (!result.Succeeded)
            return Errors.User.MapIdentityError(result.Errors.ToList());
         
        return new AuthenticationResult
        {
            Message = "Your username is successfully changed",
            Token = "Bearer " + (new JwtSecurityTokenHandler().WriteToken(_jwtTokenGenerator
                .GenerateJwtTokenAsync(
                user.Id,
                user.UserName!,
                await GetUserClaims(user)))),
            Username = user.UserName
        };
        
    }
}