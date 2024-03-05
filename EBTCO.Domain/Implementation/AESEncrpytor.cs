using EBTCO.Core.Contract;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace ToursYard.Core.Helpers
{
    public class AESEncrpytor : IAESEncryptor
    {
        private readonly IConfiguration _configuration;
        public AESEncrpytor(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Encrypt(string plainText)
        {
            byte[] MessegeBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] Key = Convert.FromBase64String(_configuration.GetSection("SecretEncryptionKey").Value ?? String.Empty);
            byte[] IV = Convert.FromBase64String(_configuration.GetSection("SecretEncryptionIV").Value ?? String.Empty);
            byte[] EncryptedBytes = encrypt(MessegeBytes, Key, IV);
            string Cipher = Convert.ToBase64String(EncryptedBytes);
            return Cipher;
        }

        public string Decrypt(string cipher)
        {
            var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_configuration.GetSection("SecretEncryptionKey").Value ?? String.Empty);
            aes.IV = Convert.FromBase64String(_configuration.GetSection("SecretEncryptionIV").Value ?? String.Empty);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            byte[] EncryptedBytes = Convert.FromBase64String(cipher);

            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            {
                byte[] DecryptedBytes = decryptor.TransformFinalBlock(EncryptedBytes, 0, EncryptedBytes.Length);
                return Encoding.ASCII.GetString(DecryptedBytes);
            }
        }

        private byte[] encrypt(byte[] data, byte[] key, byte[] vi)
        {
            var aes = Aes.Create();
            aes.Key = key;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.IV = vi;
            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            {
                return encryptor.TransformFinalBlock(data, 0, data.Length);
            }
        }
    }
}
