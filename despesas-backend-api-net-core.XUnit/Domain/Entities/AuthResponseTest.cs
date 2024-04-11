namespace Domain.Entities;
public class AuthResponseTest
{
    [Theory]
    [InlineData(true, "20231006", "31536000", "0123456789ABCDEF", "Acesso realizado com sucesso")]
    [InlineData(false, "", "", "", "Acesso não realizado")]
    [InlineData(false, null, null, null, "Acesso não realizado")]
    public void AuthResponse_Should_Set_Properties_Correctly(bool authenticated, string? created, string? expiration, string? accessToken, string message)
    {
        var mockUsuario = Mock.Of<Usuario>();

        // Arrange and ACt
        var authResponse = new Authentication 
        { 
             Authenticated = authenticated,
             Created = created,
             Expiration = expiration,
             AccessToken = accessToken,
             Message = message
        };
        
        // Assert
        Assert.Equal(authenticated, authResponse.Authenticated);
        Assert.Equal(created, authResponse.Created);
        Assert.Equal(expiration, authResponse.Expiration);
        Assert.Equal(accessToken, authResponse.AccessToken);
        Assert.Equal(message, authResponse.Message);
    }
}