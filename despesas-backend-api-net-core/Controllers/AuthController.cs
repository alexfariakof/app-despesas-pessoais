using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace despesas_backend_api_net_core.Controllers;

[Authorize("Bearer")]
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public abstract class AuthController : ControllerBase
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