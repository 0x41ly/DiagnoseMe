namespace Auth.Contracts.Authentication;

public record ChangePasswordRequest(
    string OldPassword,
    string NewPassword);