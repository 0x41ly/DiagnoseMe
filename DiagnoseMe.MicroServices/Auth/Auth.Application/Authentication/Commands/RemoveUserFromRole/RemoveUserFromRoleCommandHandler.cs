
namespace Auth.Application.Authentication.Commands.RemoveUserFromRole;

public class RemoveUserFromRoleCommandHandler :
    BaseAuthenticationHandler,
    IRequestHandler<RemoveUserFromRoleCommand, ErrorOr<AuthenticationResult>>
{
    public RemoveUserFromRoleCommandHandler(
        UserManager<ApplicationUser> userManager
    ): base(userManager){}

    public async Task<ErrorOr<AuthenticationResult>> Handle(RemoveUserFromRoleCommand command, CancellationToken cancellationToken)
    {
        var results = new AuthenticationResult();
        if (!Roles.AvailableRoles.Contains(command.Role))
            return Errors.Role.RoleNotExist;

        var user = await _userManager.FindByNameAsync(command.UserName);
        if (user == null)
            return Errors.User.Name.NotExists;
        
        var result = await _userManager.RemoveFromRoleAsync(user, command.Role);
        if(!result.Succeeded)
            return Errors.User.MapIdentityError(result.Errors.ToList());
        
        results.Message = $"User is successfully removed from {command.Role}role";
        return results;
    }
}
