using D1TechTestCase.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.Crypto
{
    public class CryptoService
    {
        public static string RandomStringGenerate(uint length)
        {
            var numberByte = new Byte[length];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }
        public static string ComputeSha256Hash(string rawData)
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));


                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {

                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static byte[] ComputeSha256HashBytes(string rawData)
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return bytes;
            }
        }
        public static string ComputeSha512Hash(string rawData)
        {

            using (SHA512 sha512Hash = SHA512.Create())
            {

                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {

                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static byte[] ComputeSha512HashBytes(string rawData)
        {

            using (SHA512 sha512Hash = SHA512.Create())
            {

                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return bytes;
            }
        }
    }
}
