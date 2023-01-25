using System.Security.Cryptography;
using System.Text;

namespace Auth.Domain.Common;

public static class GuidHelper
{

    public static string GenerateGuidForString(string input)
    {
        using(MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return (new Guid(hashBytes)).ToString();
        }
    }
}