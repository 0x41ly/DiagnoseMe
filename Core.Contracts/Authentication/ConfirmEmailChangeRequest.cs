namespace Core.Contracts.Authentication;

public record ConfirmEmailChangeRequest(
    string Id,
    string NewEmail
);