namespace Core.Contracts.Authentication;

public record ResetPasswordRequest(
    string Id,
    string NewPassword);