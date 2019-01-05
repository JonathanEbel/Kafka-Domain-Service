using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Cryptography
{
    public static class Crypto
    {
        public static string getHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static string getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public static string HashHMAC(string key, string message)
        {
            var hash = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            return BitConverter.ToString(hash.ComputeHash(Encoding.UTF8.GetBytes(message)));
        }

    }
}
