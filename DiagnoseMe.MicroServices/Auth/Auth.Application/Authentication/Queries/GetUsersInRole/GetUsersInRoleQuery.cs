namespace Auth.Application.Authentication.Queries.GetUsersInRole;


public record GetUsersInRoleQuery(
    string Role,
    int pageNumber) : IRequest<ErrorOr<List<ApplicationUser>>>;