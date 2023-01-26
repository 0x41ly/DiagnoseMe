
namespace MedicalBlog.Domain.Common.Roles;
public static partial class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Doctor = "Doctor";
    public static List<string> AvailableRoles = new List<string>() { Admin, User, Doctor };
}

