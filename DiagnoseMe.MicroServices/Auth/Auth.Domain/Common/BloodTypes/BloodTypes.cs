using System.Collections.Generic;
namespace Auth.Domain.Common.BloodTypes;

public static class BloodTypes
{
    public static List<string> bloodTypeList => new List<string>{ "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-", "I do not know" };

}