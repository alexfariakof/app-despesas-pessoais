namespace Business.Authentication.Abstractions;
public interface ITokenConfiguration
{
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string refreshToken);
}
