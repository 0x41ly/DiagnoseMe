namespace Auth.Application.Authentication.Queries.GetAllUsers;

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
                    OrderBy(u => u.UserName).
                    AsParallel().
                    Skip((query.pageNumber -1)* 10).
                    Take(10).
                    ToList();
        return Task.FromResult<List<ApplicationUser>>(users);
    }
    
}