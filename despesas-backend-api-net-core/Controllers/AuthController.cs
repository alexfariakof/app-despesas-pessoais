using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace despesas_backend_api_net_core.Controllers
{
    
    public abstract class AuthController : Controller
    {
        public AuthController() { }
        protected int IdUsuario
        {
            [Authorize("Bearer")]
            get
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = HttpContext.Request.Headers["Authorization"].ToString();
                    var jwtToken = tokenHandler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
                    var idUsuario = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "IdUsuario")?.Value.ToInteger();
                    return idUsuario.Value;
                }
                catch
                {
                    return 0;
                }

            }
        }

        protected int? GetIdUsuarioFromBearerToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
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