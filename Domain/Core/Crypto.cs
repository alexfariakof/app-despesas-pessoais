using Domain.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Core;
public class Crypto : ICrypto
{
    private readonly byte[] Key; // Chave fixa de 256 bits
    private static ICrypto? Instance;
    private static readonly object LockObject = new object();
    public static ICrypto GetInstance
    {
        get
        {
            lock (LockObject)
            {
                if (Instance == null)
                {
                    Instance = new Crypto();
                }

                return Instance;
            }
        }
    }
    private Crypto()
    {
        var key = getHashKey();
        var keyByte = Convert.FromBase64String(key);
        Key = keyByte;
    }

    private string? getHashKey()
    {
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

        if (File.Exists(jsonFilePath))
        {
            var jsonContent = File.ReadAllText(jsonFilePath);
            var config = JObject.Parse(jsonContent);
            var cryptoKey = config["Crypto"]?["Key"]?.ToString();

            return cryptoKey;
        }
        else
        {
            throw new ArgumentException("Arquivo com chave de criptografia não encontrado");
        }
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
