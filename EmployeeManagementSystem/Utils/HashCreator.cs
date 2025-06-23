using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagementSystem.Utils
{
    public static class HashCreator
    {
        public static string Sha256Hasher(string value, string additionalValue)
        {
            var hasher = SHA256.Create();
            var hashedBytes = Encoding.UTF8.GetBytes(value + additionalValue);
            var computedHash = hasher.ComputeHash(hashedBytes);
            var hashedPassword = Convert.ToBase64String(computedHash);

            return hashedPassword;
        }
    }
}
