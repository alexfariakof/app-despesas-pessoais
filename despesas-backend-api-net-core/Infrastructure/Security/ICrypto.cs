using System.Security.Cryptography;

namespace despesas_backend_api_net_core.Infrastructure.Security.Implementation
{
    public interface ICrypto
    {
        string Encrypt(string password);
        string Decrypt(string encryptedText);
    }
}