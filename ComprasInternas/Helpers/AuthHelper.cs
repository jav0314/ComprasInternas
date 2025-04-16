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
        public static bool Verificar(string password, string hashGuardado)
        {
            var combined = Convert.FromBase64String(hashGuardado);
            var salt = new byte[16];
            Buffer.BlockCopy(combined, 0, salt, 0, 16);

            var hash = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            var hashBytes = hash.GetBytes(32);

            for (int i = 0; i < 32; i++)
            {
                if (combined[i + 16] != hashBytes[i])
                    return false;
            }

            return true;
        }
    }
}
