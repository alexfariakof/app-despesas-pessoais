using System.Security.Cryptography;
using System.Text;

namespace despesas_backend_api_net_core.Infrastructure.Security.Implementation
{
    public interface ICrypto
    {
        public string Encrypt(string password);

        public string Decrypt(string encryptedText);

    }
}