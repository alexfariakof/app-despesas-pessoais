using System.IdentityModel.Tokens.Jwt;
using Business.Authentication.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Business.Authentication;
public class TokenConfiguration: ITokenConfiguration
{
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public int Seconds { get; set; }
    public int DaysToExpiry { get; set; }

    public string GenerateRefreshToken()
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor()
        {
            Audience = Audience,
            Issuer = Issuer,
            Claims = new Dictionary<string, object> { { "KEY", Guid.NewGuid() } },
            Expires = DateTime.UtcNow.AddDays(DaysToExpiry)
            //Expires = DateTime.UtcNow.AddSeconds(60)
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
