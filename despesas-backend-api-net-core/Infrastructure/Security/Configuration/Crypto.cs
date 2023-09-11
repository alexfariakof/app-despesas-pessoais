using System.Security.Cryptography;
using System.Text;

namespace despesas_backend_api_net_core.Infrastructure.Security.Configuration
{
    public class Crypto
    {
        private static byte[] Key; // Chave fixa de 256 bits
        private static Crypto? Instance;
        private Crypto() { }
        public static Crypto GetInstance 
        {
            get
            {
                return Instance == null ? new() : Instance;
            }
        
        }

        public void SetCryptoKey(string _key)
        {
            Key = Convert.FromBase64String(_key);
        }
        public string Encrypt(string password)
        {
            byte[] iv = GenerateIV();

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] encryptedBytes = PerformCryptography(password, encryptor);

                byte[] encryptedData = new byte[iv.Length + encryptedBytes.Length];
                Buffer.BlockCopy(iv, 0, encryptedData, 0, iv.Length);
                Buffer.BlockCopy(encryptedBytes, 0, encryptedData, iv.Length, encryptedBytes.Length);

                return Convert.ToBase64String(encryptedData);
            }
        }

        public string Decrypt(string encryptedText)
        {
            byte[] encryptedData = Convert.FromBase64String(encryptedText);
            byte[] iv = new byte[16];
            byte[] encryptedBytes = new byte[encryptedData.Length - 16];

            Buffer.BlockCopy(encryptedData, 0, iv, 0, 16);
            Buffer.BlockCopy(encryptedData, 16, encryptedBytes, 0, encryptedData.Length - 16);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] decryptedBytes = PerformCryptography(encryptedBytes, decryptor);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        private static byte[] GenerateIV()
        {
            byte[] iv = new byte[16];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(iv);
            }
            return iv;
        }

        private static byte[] PerformCryptography(string data, ICryptoTransform transform)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(data);
            return PerformCryptography(inputBytes, transform);
        }

        private static byte[] PerformCryptography(byte[] data, ICryptoTransform transform)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
}