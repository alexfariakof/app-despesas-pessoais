using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Business.Authentication.Abstractions;
public interface ISigningConfigurations
{
    public SecurityKey? Key { get; }
    string CreateAccessToken(ClaimsIdentity identity);
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string refreshToken);
}
