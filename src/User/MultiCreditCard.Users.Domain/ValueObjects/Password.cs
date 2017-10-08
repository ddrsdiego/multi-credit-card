using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MultiCreditCard.Users.Domain.ValueObjects
{
    public class Password
    {
        public Password(string password)
        {
            Raw = password;
            Encoded = EncodePassword(password);
        }

        public string Encoded { get; private set; }
        public string Raw { get; private set; }

        private static string EncodePassword(string password)
        {
            string result;
            var bytes = Encoding.Unicode.GetBytes(password);

            using (var stream = new MemoryStream())
            {
                stream.WriteByte(0);

                using (var sha256 = new SHA256Managed())
                {
                    var hash = sha256.ComputeHash(bytes);
                    stream.Write(hash, 0, hash.Length);

                    bytes = stream.ToArray();
                    result = Convert.ToBase64String(bytes);
                }

            }
            return result;
        }
    }
}
