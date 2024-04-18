using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace despesas_backend_api_net_core.Controllers
{
    [Authorize("Bearer")]
    public abstract class AuthController : Controller
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

        protected int? GetIdUsuarioFromBearerToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                var idUsuario = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "IdUsuario")?.Value.ToInteger();
                return idUsuario.Value;
            }
            catch
            {
                return 0;
            }
        }
    }
}