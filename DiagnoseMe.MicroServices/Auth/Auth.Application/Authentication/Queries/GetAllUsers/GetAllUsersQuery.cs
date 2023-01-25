namespace Auth.Application.Authentication.Queries.GetAllUsers;

public record GetAllUsersQuery(
    int pageNumber
) : IRequest<List<ApplicationUser>>;