using System.Security.Cryptography;

namespace ComprasInternas.Helpers
{
    public class AuthHelper
    {
        public static string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var hash = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hashBytes = hash.GetBytes(32);

            var result = new byte[48];
            Buffer.BlockCopy(salt, 0, result, 0, 16);
            Buffer.BlockCopy(hashBytes, 0, result, 16, 32);

            return Convert.ToBase64String(result);
        }
    }
}
