namespace Auth.Application.Authentication.Commands.ChangePassword;

public class ChangePasswordCommandHandler :
    BaseAuthenticationHandler,
    IRequestHandler<ChangePasswordCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ISmtp _smtp;
    public ChangePasswordCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        UserManager<ApplicationUser> userManager,
        ISmtp smtp) : base(userManager)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _smtp = smtp;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(command.UserName);

        if(!user!.EmailConfirmed)
            return Errors.User.Email.NotConfirmed;

        var result = await _userManager.ChangePasswordAsync(
            user, 
            command.OldPassword, 
            command.NewPassword);
        if(result.Succeeded)
            return Errors.User.MapIdentityError(result.Errors.ToList());
        
        try{
            await _smtp.SendEmailAsync(
                new MailAddress(user.Email!,user.UserName),
                "Password changed",
                "Your password has been changed successfully");
        }
        catch
        {
            // TODO: Log this error
            Console.WriteLine("Failed to send email");
        }
        return new AuthenticationResult
        {
            Message = "Password changed successfully",
            Token = "Bearer " + (new JwtSecurityTokenHandler().WriteToken(_jwtTokenGenerator
                .GenerateJwtTokenAsync(
                user.Id,
                user.UserName!,
                await GetUserClaims(user)))),
            Username = user.UserName
        };
    }
}