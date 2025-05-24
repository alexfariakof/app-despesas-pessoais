using Business.Authentication.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Business.Authentication;
public class SigningConfigurations : ISigningConfigurations
{
    public SecurityKey? Key { get; }
    public TokenConfiguration? TokenConfiguration { get; }
    public SigningCredentials? SigningCredentials { get; private set; }
    
    public SigningConfigurations(IOptions<TokenOptions> options )
    {
        TokenConfiguration = new TokenConfiguration(options);

        if (!String.IsNullOrEmpty(options.Value.Certificate))
        {
            string certificatePath = Path.Combine(AppContext.BaseDirectory, options.Value.Certificate);
            X509Certificate2 certificate = new X509Certificate2(certificatePath, options.Value.Password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
            RSA? rsa = null;
            rsa = certificate.GetRSAPrivateKey();
            RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(rsa);
            rsaSecurityKey.KeyId = Guid.NewGuid().ToString();
            SigningCredentials signingCredentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256Signature);
            Key = rsaSecurityKey;
            SigningCredentials = signingCredentials;
        }
        else
        {
            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
                SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
            }
        }
    }

    public string CreateAccessToken(ClaimsIdentity identity)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = TokenConfiguration.Issuer,
            Audience = TokenConfiguration.Audience,
            SigningCredentials = SigningCredentials,
            Subject = identity,
            NotBefore = DateTime.UtcNow,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddSeconds(TokenConfiguration.Seconds),
        });

        string token = handler.WriteToken(securityToken);
        return token;
    }

    public string GenerateRefreshToken()
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor()
        {
            Audience = TokenConfiguration.Audience,
            Issuer = TokenConfiguration.Issuer,
            Claims = new Dictionary<string, object> { { "KEY", Guid.NewGuid() } },
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddSeconds(TokenConfiguration.Seconds),
        });

        return handler.WriteToken(securityToken);
    }

    public bool ValidateRefreshToken(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(refreshToken.Replace("Bearer ", "")) as JwtSecurityToken;
        return jwtToken?.ValidTo >= DateTime.UtcNow;
    }
}