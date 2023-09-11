using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace despesas_backend_api_net_core.Infrastructure.Security.Configuration
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; } 
        public SigningCredentials SigningCredentials { get;  }

        public SigningConfigurations()
        {
            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
