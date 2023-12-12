using Xunit.Extensions.Ordering;

namespace Infrastructure.Common
{
    [Order(200)]
    public class DbSettingsTest
    {
        [Fact]
        public void DbSettings_Should_Set_Connection_String_Correctly()
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
