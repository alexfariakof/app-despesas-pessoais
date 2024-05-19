namespace Business.Authentication.Interfaces;
public interface ITokenConfiguration
{
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string refreshToken);
}
