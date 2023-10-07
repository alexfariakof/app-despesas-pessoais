using despesas_backend_api_net_core.Domain.Entities;


namespace Test.XUnit.Domain.Entities
{
    public class AuthResponseTest
    {
        [Theory]
        [InlineData(true, "20231006", "31536000", "0123456789ABCDEF", "Acesso realizado com sucesso", typeof(Usuario))]
        [InlineData(false, "", "", "", "Acesso não realizado", typeof(Usuario))]
        [InlineData(false, null, null, null, "Acesso não realizado", typeof(Usuario))]
        public void AuthResponse_ShouldSetPropertiesCorrectly(bool authenticated, string created, string expiration, string accessToken, string message, Type type)
        {
            var mockUsuario = Mock.Of<Usuario>();

            // Arrange and ACt
            var authResponse = new AuthResponse 
            { 
                 Authenticated = authenticated,
                 Created = created,
                 Expiration = expiration,
                 AccessToken = accessToken,
                 Message = message,
                 Usuario = mockUsuario
            };
            
            // Assert
            Assert.Equal(authenticated, authResponse.Authenticated);
            Assert.Equal(created, authResponse.Created);
            Assert.Equal(expiration, authResponse.Expiration);
            Assert.Equal(accessToken, authResponse.AccessToken);
            Assert.Equal(message, authResponse.Message);
            Assert.Equal(mockUsuario, authResponse.Usuario);
        }
    }
}