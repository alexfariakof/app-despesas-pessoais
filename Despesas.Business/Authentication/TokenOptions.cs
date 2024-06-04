namespace Business.Authentication;

public class TokenOptions
{
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public int Seconds { get; set; }
    public int DaysToExpiry { get; set; }
}
