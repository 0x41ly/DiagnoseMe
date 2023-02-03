namespace Auth.Application.Authentication.Queries.GetToken;

public class GetTokenQueryHandler: 
    BaseAuthenticationHandler,
    IRequestHandler<GetTokenQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public GetTokenQueryHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator
    ): base(userManager)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(GetTokenQuery query, CancellationToken cancellationToken)
    {
        AuthenticationResult results = new AuthenticationResult();
        var user = await _userManager.FindByEmailAsync(query.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, query.Password))
            return Errors.User.Credential.Invalid;
 
        if (!user.EmailConfirmed)
            return Errors.User.Email.NotConfirmed;
            
        results.Message = "Signed in successfully";
        results.Username = user.UserName;
        results.Token = "Bearer " + (new JwtSecurityTokenHandler().WriteToken(_jwtTokenGenerator
            .GenerateJwtTokenAsync(
            user.Id,
            user.UserName!,
            await GetUserClaims(user))));

        return results;
    }
}