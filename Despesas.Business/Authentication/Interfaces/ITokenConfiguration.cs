namespace Business.Authentication.Interfaces;
public interface ITokenConfiguration
{
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public int Seconds { get; set; }
    public int DaysToExpiry { get; set; }
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string refreshToken);
}
