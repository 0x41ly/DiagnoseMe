namespace Auth.Application.Authentication.Queries.GetUsersInRole;

public class GetUsersInRoleQueryHandler :
    BaseAuthenticationHandler,
    IRequestHandler<GetUsersInRoleQuery, ErrorOr<List<ApplicationUser>>>
{
    public GetUsersInRoleQueryHandler(
        UserManager<ApplicationUser> userManager
    ): base(userManager){}
    public async Task<ErrorOr<List<ApplicationUser>>> Handle(GetUsersInRoleQuery query, CancellationToken cancellationToken)
    {
        if (!Roles.AvailableRoles.Contains(query.Role))
            return Errors.Role.RoleNotExist;
        
        var users = (await _userManager.
                    GetUsersInRoleAsync(query.Role)).
                    OrderBy(u => u.UserName).
                    Skip((query.pageNumber -1)* 10).
                    Take(10).
                    AsParallel().
                    ToList();
        return  users;
    }
}