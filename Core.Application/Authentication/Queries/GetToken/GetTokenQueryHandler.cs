namespace Core.Application.Authentication.Queries.GetToken;

public class GetTokenQueryHandler: 
    BaseHandler,
    IRequestHandler<GetTokenQuery, ErrorOr<AuthenticationResults>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public GetTokenQueryHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator
    ): base(userManager)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResults>> Handle(GetTokenQuery query, CancellationToken cancellationToken)
    {
        AuthenticationResults results = new AuthenticationResults();
        var user = await _userManager.FindByEmailAsync(query.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, query.Password))
            return Errors.User.Credential.Invalid;
        
        var userRoles = await _userManager.GetRolesAsync(user) as List<string>;
        var userClaims = await GetUserClaims(user);
        var token = _jwtTokenGenerator.GenerateJwtTokenAsync(user.Id, user.UserName ,userClaims);
        results.Message = "Signed in successfully";
        results.Username = user.UserName;
        results.Token = new JwtSecurityTokenHandler().WriteToken(token);

        return results;
    }
}