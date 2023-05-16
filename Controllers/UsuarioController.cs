using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private IBusiness<Usuario> _usuarioBusiness;

        public UsuarioController(IBusiness<Usuario> usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;            
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
           return Ok(_usuarioBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]
        [HttpPost("GetById")]
        public IActionResult Post([FromBody] int id)
        {
            Usuario _usuario = _usuarioBusiness.FindById(id);

            if (_usuario == null)
                return NotFound();

            return Ok(_usuario);
        }

        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] UsuarioVO usuarioVO)
        {

            var usuario = new Usuario
            {
                Id = usuarioVO.Id,
                Nome = usuarioVO.Nome,
                SobreNome = usuarioVO.SobreNome,
                Email = usuarioVO.Email,
                Telefone = usuarioVO.Telefone,
                StatusUsuario = StatusUsuario.Ativo
            };

            if (usuario == null)
                return BadRequest();
            return new ObjectResult(_usuarioBusiness.Create(usuario));
        }

        [HttpPut]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] UsuarioVO usuarioVO)
        {

            var usuario = new Usuario
            {
                Id = usuarioVO.Id,
                Nome = usuarioVO.Nome,
                SobreNome = usuarioVO.SobreNome,
                Email = usuarioVO.Email,
                Telefone = usuarioVO.Telefone,
                StatusUsuario = StatusUsuario.Ativo
            };

            if (usuario == null)
                return BadRequest();

            Usuario updateUsuario = _usuarioBusiness.Update(usuario);
            if (updateUsuario == null)
                return NoContent();

            return new ObjectResult(updateUsuario);
        }

        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            _usuarioBusiness.Delete(id);
            return NoContent();
        }
    }
}
