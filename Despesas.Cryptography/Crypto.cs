using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography;

public class Crypto : ICrypto
{
    private readonly byte[] Key; // Chave fixa de 256 bits    
    private static readonly object LockObject = new object();
    private static ICrypto? _crypto;

    public static ICrypto Instance

    {
        get
        {
            lock (LockObject)
            {
                if (_crypto == null)
                {
                    _crypto = new Crypto();
                }

                return _crypto;
            }
        }
    }

    private Crypto()
    {
        var key = CreateHashKey();
        var keyByte = Convert.FromBase64String(key);
        this.Key = keyByte;
    }

    public Crypto(IOptions<CryptoOptions> options)
    {
        var key = options?.Value?.Key?.ToUpper() ?? "";
        var keyByte = Convert.FromBase64String(key);
        this.Key = keyByte;
    }

    private string CreateHashKey()
    {
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        if (File.Exists(jsonFilePath))
        {
            var jsonContent = File.ReadAllText(jsonFilePath);
            var config = JObject.Parse(jsonContent);
            var cryptoKey = config["Crypto"]?["Key"]?.ToString() ?? "";
            ValidateKey(cryptoKey);
            return cryptoKey;
        }
        else
        {
            throw new ArgumentException("File appsettings.json não encontrado.");
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

    private string Decrypt(string encryptedText)
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

    public bool IsEquals(string plaintText, string encryptedText)
    {
        return this.Decrypt(encryptedText) == plaintText;
    }

    private static byte[] GenerateIV()
    {
        byte[] iv = new byte[16];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(iv);
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

    private void ValidateKey(string cryptoKey)
    {
        if (!cryptoKey.All(IsHexadecimalDigit))
            throw new ArgumentException("A chave obtida contém caracteres inválidos.");


        if (cryptoKey.Length != 32)
            throw new ArgumentException("A chave obtida não é uma string válida da Base-64.");
    }

    private bool IsHexadecimalDigit(char c)
    {
        return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
    }
}