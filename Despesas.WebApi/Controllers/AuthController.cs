using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Despesas.WebApi.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public abstract class AuthController : ControllerBase
{
    public AuthController() { }
    protected Guid UserIdentity
    {
        get
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var jwtToken = tokenHandler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
            Guid.TryParse(jwtToken?.Claims?.FirstOrDefault(c => c.Type == "sub")?.Value, out var idUsuario);
            return idUsuario;
        }
    }
}