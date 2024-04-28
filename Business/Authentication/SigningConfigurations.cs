using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Business.Authentication;
public class SigningConfigurations
{
    public SecurityKey Key { get; }
    public SigningCredentials SigningCredentials { get; }

    public SigningConfigurations()
    {
        using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
        {
            Key = new RsaSecurityKey(provider.ExportParameters(true));
        }

        SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
    }

    public string GenerateAccessToken(ClaimsIdentity identity, TokenConfiguration tokenConfiguration, int idUsuario)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = tokenConfiguration.Issuer,
            Audience = tokenConfiguration.Audience,
            SigningCredentials = this.SigningCredentials,
            Subject = identity,
            NotBefore = DateTime.Now,            
            IssuedAt = DateTime.Now,
            Expires = DateTime.Now.AddSeconds(tokenConfiguration.Seconds),
            Claims = new Dictionary<string, object> { { "IdUsuario", idUsuario } },
        });

        string token = handler.WriteToken(securityToken);
        return token;
    }
}
