using Despesas.WebApi.Controllers;
using System.Reflection;

namespace Api.Controllers;
public sealed class AuthControllerTest
{
    private readonly AuthController _authController;

    public AuthControllerTest()
    {
        _authController = new Mock<AuthController>().Object;
    }

    [Fact]
    public void UserIdentity_ShouldReturnCorrectUserId()
    {
        // Arrange
        Guid mockIdUsuario = Guid.NewGuid();
        Usings.SetupBearerToken(mockIdUsuario, _authController);

        // Act
        var result = GetProtectedProperty<Guid>(_authController, "UserIdentity");

        // Assert
        Assert.Equal(mockIdUsuario, result);
    }

    private T? GetProtectedProperty<T>(object obj, string propertyName)
    {
        var propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);

        return (T?)propertyInfo.GetValue(obj) ;
    }
}
