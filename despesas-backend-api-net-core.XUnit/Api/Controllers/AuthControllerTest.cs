using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace Api.Controllers;
public class AuthControllerTest
{
    protected readonly AuthController _authController;

    private void SetupBearerToken(int userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };
        var identity = new ClaimsIdentity(claims, "IdUsuario");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers["Authorization"] =
            "Bearer " + Usings.GenerateJwtToken(userId);

        _authController.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }

    public AuthControllerTest()
    {
        _authController = new AuthController();
    }

    public void IdUsuario_ShouldReturnCorrectUserId()
    {
        // Arrange
        const int mockIdUsuario = 22;
        SetupBearerToken(mockIdUsuario);

        // Act
        var result = GetProtectedProperty<int>(_authController, "IdUsuario");

        // Assert
        Assert.Equal(mockIdUsuario, result);
    }

    private T GetProtectedProperty<T>(object obj, string propertyName)
    {
        var propertyInfo = obj.GetType()
            .GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);

        return (T)propertyInfo.GetValue(obj);
    }
}
