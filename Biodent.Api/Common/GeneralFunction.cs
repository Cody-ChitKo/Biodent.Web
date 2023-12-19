using System.Security.Cryptography;

namespace Biodent.Api.Common
{
    public class GeneralFunction
    {
        private const int SaltSize = 32;
        public static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[SaltSize];
                rng.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] ComputerMAC256(byte[] data, byte[] salt)
        {
            using (var hmac = new HMACSHA256(salt))
            {
                return hmac.ComputeHash(data);
            }
        }
    }
}
