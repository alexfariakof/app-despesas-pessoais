using despesas_backend_api_net_core.Infrastructure.Data.Common;
using Xunit;

namespace Test.XUnit.Infrastructure.Data.Common
{
    public class DbSettingsTest
    {
        [Fact]
        public void DbSettings_ShouldSetConnectionStringCorrectly()
        {
            // Arrange
            var dbSettings = new DbSettings();

            // Act
            dbSettings.ConnectionString = "Test Connection String";

            // Assert
            Assert.Equal("Test Connection String", dbSettings.ConnectionString);
        }
    }
}
