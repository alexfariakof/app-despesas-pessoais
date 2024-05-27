using System.IdentityModel.Tokens.Jwt;
using Business.Authentication.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Business.Authentication;
public class TokenConfiguration: ITokenConfiguration
{
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public int Seconds { get; set; }
    public int DaysToExpiry { get; set; }

    public TokenConfiguration(IOptions<TokenOptions> options)
    {
        this.Audience = options.Value.Audience;
        this.Issuer = options.Value.Issuer;
        this.Seconds = options.Value.Seconds;
        this.DaysToExpiry = options.Value.DaysToExpiry;
    }

    public string GenerateRefreshToken()
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor()
        {
            Audience = this.Audience,
            Issuer = this.Issuer,
            Claims = new Dictionary<string, object> { { "KEY", Guid.NewGuid() } },
            Expires = DateTime.UtcNow.AddDays(this.DaysToExpiry)
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
