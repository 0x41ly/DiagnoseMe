namespace Core.Application.Authentication.Queries.GetUsersInRole;

public class GetUsersInRoleQueryHandler :
    BaseHandler,
    IRequestHandler<GetUsersInRoleQuery, ErrorOr<List<ApplicationUser>>>
{
    public GetUsersInRoleQueryHandler(
        UserManager<ApplicationUser> userManager
    ): base(userManager){}
    public async Task<ErrorOr<List<ApplicationUser>>> Handle(GetUsersInRoleQuery query, CancellationToken cancellationToken)
    {
        if (!Roles.AvailableRoles.Contains(query.Role))
            return Errors.Role.RoleNotExist;
        
        var users = await _userManager.GetUsersInRoleAsync(query.Role);
        return users.AsParallel().ToList();
    }
}