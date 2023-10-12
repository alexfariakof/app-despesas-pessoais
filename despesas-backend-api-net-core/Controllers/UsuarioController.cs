using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private IUsuarioBusiness _usuarioBusiness;
        private string bearerToken;
        public UsuarioController(IUsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
            bearerToken = String.Empty;
        }

        [HttpGet("{IdUsuario}")]
        [Authorize("Bearer")]
        public IActionResult Get([FromRoute]int idUsuario)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();
            if (_idUsuario != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            var usuario = _usuarioBusiness.FindById(_idUsuario);
            if (usuario.PerfilUsuario != PerfilUsuario.Administrador)
                return Ok(new List<UsuarioVM>());

            return Ok(_usuarioBusiness.FindAll(_idUsuario));
        }

        [Authorize("Bearer")]
        [HttpPost("GetById")]
        public IActionResult Post([FromBody] int idUsuario)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario =  bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            UsuarioVM _usuario = _usuarioBusiness.FindById(idUsuario);
            if (_usuario == null)
                return BadRequest(new { message ="Usuário não encontrado!" });

            return Ok(_usuario);
        }

        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] UsuarioVM usuarioVM)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario =  bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != usuarioVM.Id)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (String.IsNullOrEmpty(usuarioVM.Telefone) || String.IsNullOrWhiteSpace(usuarioVM.Telefone))
                return BadRequest("Campo Telefone não pode ser em branco");

            if (String.IsNullOrEmpty(usuarioVM.Email) || String.IsNullOrWhiteSpace(usuarioVM.Email))
                return BadRequest("Campo Login não pode ser em branco");

            if (!IsValidEmail(usuarioVM.Email))
                return BadRequest(new { message = "Email inválido!" });

            return new OkObjectResult(_usuarioBusiness.Create(usuarioVM));
        }

        [HttpPut]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] UsuarioVM usuarioVM)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != usuarioVM.Id)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (String.IsNullOrEmpty(usuarioVM.Telefone) || String.IsNullOrWhiteSpace(usuarioVM.Telefone))
                return BadRequest("Campo Telefone não pode ser em branco");

            if (String.IsNullOrEmpty(usuarioVM.Email) || String.IsNullOrWhiteSpace(usuarioVM.Email))
                return BadRequest("Campo Login não pode ser em branco");

            if (!IsValidEmail(usuarioVM.Email))
                return BadRequest(new { message = "Email inválido!" });

            UsuarioVM updateUsuario = _usuarioBusiness.Update(usuarioVM);
            if (updateUsuario == null)
                return  BadRequest(new { message = "Usuário não encontrado!" });

            return new OkObjectResult(updateUsuario);
        }

        [HttpDelete]
        [Authorize("Bearer")]
        public IActionResult Delete([FromBody] UsuarioVM usuarioVM, [FromBody] int idUsuario)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario =  bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            UsuarioVM _usuario = _usuarioBusiness.FindById(usuarioVM.Id);
            if (_usuario.PerfilUsuario != PerfilUsuario.Administrador)
                return BadRequest(new { message = "Usuário não possui permissão para exectar deleção!" });
                
            if (_usuarioBusiness.Delete(usuarioVM))
                return new OkObjectResult(new { message = true });
            else
                return BadRequest(new { message = "Erro ao excluir Usuário!" });
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
