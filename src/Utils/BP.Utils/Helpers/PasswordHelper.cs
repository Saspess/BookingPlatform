using System.Security.Cryptography;
using System.Text;
using BP.Utils.Constants;

namespace BP.Utils.Helpers
{
    public static class PasswordHelper
    {
        public static string GenerateSalt()
        {
            var salt = new byte[PasswordValues.SaltBytes];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashedPasswordBytes = SHA256.HashData(passwordBytes);
            return $"{Convert.ToBase64String(hashedPasswordBytes)}{PasswordValues.Separator}{salt}";
        }

        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            var hashParts = hashedPassword.Split(PasswordValues.Separator);
            var salt = hashParts[PasswordValues.SaltPosition] ?? throw new ArgumentException(ExceptionMessges.SaltNotFound);
            return hashedPassword == HashPassword(inputPassword, salt);
        }
    }
}
