using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Domain;

namespace Despesas.WebApi.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public abstract class AuthController : ControllerBase
{
    public AuthController() { }
    protected int UserIdentity
    {
        get
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var jwtToken = tokenHandler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
            var idUsuario = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "sub")?.Value.ToInteger();
            return idUsuario.Equals(null) ? 0 : idUsuario.Value;
        }
    }
}