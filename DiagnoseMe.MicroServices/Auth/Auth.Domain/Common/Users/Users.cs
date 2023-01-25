namespace Auth.Domain.Common.Users;

public static class Users
{
    public const string Admin = "Admin";
    public static string AdminId = GuidHelper.GenerateGuidForString(Admin);
    public const string User = "User";
    public static string UserId = GuidHelper.GenerateGuidForString(User);
}