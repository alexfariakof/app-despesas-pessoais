using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Despesas.WebApi.Controllers.v1;

[ApiController]
public abstract class AuthController : ControllerBase
{
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