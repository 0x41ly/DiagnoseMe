
namespace Core.Contracts.Authentication;

public record AuthenticationResponse(
    string Message,
    bool IsSuccess,
    string Username,
    List<string> Roles,
    string Toke);

