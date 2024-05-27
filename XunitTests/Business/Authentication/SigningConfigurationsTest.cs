using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Business.Authentication;
public sealed class SigningConfigurationsTest
{
    [Fact]
    public void SigningConfigurations_Should_Initialize_Correctly()
    {
        // Arrange & Act
        var options = Options.Create(new TokenOptions
        {
            Issuer = "XUnit-Issuer",
            Audience = "XUnit-Audience",
            Seconds = 3600
        });

        var signingConfigurations = new SigningConfigurations(options);

        // Assert
        Assert.NotNull(signingConfigurations.Key);
        Assert.NotNull(signingConfigurations.SigningCredentials);
    }

    [Fact]
    public void Key_Should_Be_RSA_SecurityKey()
    {
        // Arrange
        var options = Options.Create(new TokenOptions
        {
            Issuer = "XUnit-Issuer",
            Audience = "XUnit-Audience",
            Seconds = 3600
        });

        var signingConfigurations = new SigningConfigurations(options);

        // Assert
        Assert.IsType<RsaSecurityKey>(signingConfigurations.Key);
    }

    [Fact]
    public void SigningCredentials_Should_Be_Correct_Algorithm()
    {
        // Arrange
        var options = Options.Create(new TokenOptions
        {
            Issuer = "XUnit-Issuer",
            Audience = "XUnit-Audience",
            Seconds = 3600
        });

        var signingConfigurations = new SigningConfigurations(options);

        // Assert
        Assert.NotNull(signingConfigurations.SigningCredentials.Algorithm);
        Assert.Equal(SecurityAlgorithms.RsaSha256Signature, signingConfigurations.SigningCredentials.Algorithm);
    }
}
