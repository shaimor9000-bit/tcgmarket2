using System;
using System.Security.Cryptography;

namespace DAL
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16;   // 128 ביט
        private const int HashSize = 32;   // 256 ביט
        private const int Iterations = 10000;

        // יוצר מלח (salt) רנדומלי חדש ומחשב האש לסיסמה איתו
        public static void CreateHash(string plainPassword, out string hash, out string salt)
        {
            var saltBytes = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            var hashBytes = Hash(plainPassword, saltBytes);

            salt = Convert.ToBase64String(saltBytes);
            hash = Convert.ToBase64String(hashBytes);
        }

        // מחשב מחדש את ההאש לפי הסיסמה שהוקלדה + המלח השמור, ומשווה מול מה ששמור ב-DB
        public static bool Verify(string plainPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var computedHash = Convert.ToBase64String(Hash(plainPassword, saltBytes));

            return computedHash == storedHash;
        }

        private static byte[] Hash(string plainPassword, byte[] saltBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, saltBytes, Iterations))
            {
                return pbkdf2.GetBytes(HashSize);
            }
        }
    }
}