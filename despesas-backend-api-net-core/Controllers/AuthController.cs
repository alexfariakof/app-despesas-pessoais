using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace despesas_backend_api_net_core.Controllers;

[Authorize("Bearer")]
public abstract class AuthController : ValuesController
{
    public AuthController() { }
    protected int IdUsuario
    {
        get
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var jwtToken = tokenHandler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
            var idUsuario = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "IdUsuario")?.Value.ToInteger();
            return idUsuario.Equals(null) ? 0 : idUsuario.Value;
        }
    }
}