using Microsoft.Extensions.Options;

namespace Business.Authentication;
public class TokenConfiguration
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
}
