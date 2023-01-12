namespace Core.Application.Authentication.Queries.GetAllUsers;

public class GetAllUsersQueryHandler :
    BaseAuthenticationHandler,
    IRequestHandler<GetAllUsersQuery, List<ApplicationUser>>
{
    public GetAllUsersQueryHandler(
        UserManager<ApplicationUser> userManager
    ): base(userManager){}
    public Task<List<ApplicationUser>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var users = _userManager.
                    Users.
                    AsParallel().
                    ToList();
        return Task.FromResult<List<ApplicationUser>>(users);
    }
    
}