
namespace Auth.Domain.Common.Roles;
public static partial class Roles
{
    public const string Admin = "Admin";
    public static string AdminId = GuidHelper.GenerateGuidForString(Admin);
    public const string User = "User";
    public static string UserId = GuidHelper.GenerateGuidForString(User);
    public const string Doctor = "Doctor";
    public static string DoctorId = GuidHelper.GenerateGuidForString(Doctor);

    public static List<string> AvailableRoles = new List<string>() { Admin, User, Doctor };
}

