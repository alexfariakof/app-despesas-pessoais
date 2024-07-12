using Microsoft.Extensions.Options;

namespace Business.Authentication;
public sealed class TokenConfigurationTest
{
    [Fact]
    public void Properties_Should_Be_Set_Correctly()
    {
        // Arrange
        var options = Options.Create(new TokenConfiguration
        {
            Issuer = "TesteIssuer",
            Audience = "TesteAudience",
            Seconds = 3600
        });

        // Act & Assert
        Assert.Equal("TesteAudience", options.Value.Audience);
        Assert.Equal("TesteIssuer", options.Value.Issuer);
        Assert.Equal(3600, options.Value.Seconds);
    }
}
