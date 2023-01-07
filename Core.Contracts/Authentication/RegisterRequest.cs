using System;


namespace Core.Contracts.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string UserName,
    string NationalID,
    string Gender,
    string DateOfBirth,
    string BloodType,
    string Email,
    string Password
);

