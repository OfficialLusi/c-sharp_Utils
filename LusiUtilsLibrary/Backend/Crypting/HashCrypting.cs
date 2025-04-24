using System.Security.Cryptography;

namespace LusiUtilsLibrary.Backend.Crypting
{
    public class HashCrypting
    {
        public static byte[] GenerateSalt(int length = 16)
        {
            byte[] salt = new byte[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static byte[] HashPassword(string password, byte[] salt, int iterations = 10000, int hashByteSize = 20)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA1))
            {
                return pbkdf2.GetBytes(hashByteSize);
            }
        }

        public static byte[] CalculateHash(string password, byte[] salt)
        {
            byte[] hash = HashCrypting.HashPassword(password, salt);

            return hash;
        }
    }
}
