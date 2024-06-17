using Microsoft.Extensions.Options;

namespace Business.Authentication;
public sealed class TokenConfigurationTest
{
    [Fact]
    public void Properties_Should_Be_Set_Correctly()
    {
        // Arrange
        var options = Options.Create(new TokenOptions
        {
            Issuer = "TesteIssuer",
            Audience = "TesteAudience",
            Seconds = 3600
        });

        // Act
        var tokenConfiguration = new TokenConfiguration(options);

        // Assert
        Assert.Equal("TesteAudience", tokenConfiguration.Audience);
        Assert.Equal("TesteIssuer", tokenConfiguration.Issuer);
        Assert.Equal(3600, tokenConfiguration.Seconds);
    }
}
