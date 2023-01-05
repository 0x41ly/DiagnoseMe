using System;


namespace Core.Contracts.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password
);

