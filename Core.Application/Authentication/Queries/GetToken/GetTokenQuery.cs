namespace Core.Application.Authentication.Queries.GetToken;

public record GetTokenQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResults>>;