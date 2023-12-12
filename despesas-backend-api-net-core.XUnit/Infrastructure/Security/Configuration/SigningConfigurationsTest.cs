using despesas_backend_api_net_core.Infrastructure.Security.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security.Configuration
{
    public class SigningConfigurationsTest
    {
        [Fact]
        public void SigningConfigurations_Should_Initialize_Correctly()
        {
            // Arrange & Act
            var signingConfigurations = new SigningConfigurations();

            // Assert
            Assert.NotNull(signingConfigurations.Key);
            Assert.NotNull(signingConfigurations.SigningCredentials);
        }

        [Fact]
        public void Key_Should_Be_RSA_SecurityKey()
        {
            // Arrange
            var signingConfigurations = new SigningConfigurations();

            // Assert
            Assert.IsType<RsaSecurityKey>(signingConfigurations.Key);
        }

        [Fact]
        public void SigningCredentials_Should_Be_Correct_Algorithm()
        {
            // Arrange
            var signingConfigurations = new SigningConfigurations();

            // Assert
            Assert.NotNull(signingConfigurations.SigningCredentials.Algorithm);
            Assert.Equal(SecurityAlgorithms.RsaSha256Signature, signingConfigurations.SigningCredentials.Algorithm);
        }
    }
}
