using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private IBusiness<UsuarioVM> _usuarioBusiness;

        public UsuarioController(IBusiness<UsuarioVM> usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;            
        }

        [HttpGet]
        //[Authorize("Bearer")]
        public IActionResult Get()
        {
           return Ok(_usuarioBusiness.FindAll());
        }

        //[Authorize("Bearer")]
        [HttpPost("GetById")]
        public IActionResult Post([FromBody] int idUsuario)
        {
            UsuarioVM _usuario = _usuarioBusiness.FindById(idUsuario);

            if (_usuario == null)
                return NotFound();

            return Ok(_usuario);
        }

        [HttpPost]
        //[Authorize("Bearer")]
        public IActionResult Post([FromBody] UsuarioVM usuarioVO)
        {

            if (usuarioVO == null)
                return BadRequest();
            return new ObjectResult(_usuarioBusiness.Create(usuarioVO));
        }

        [HttpPut]
        //[Authorize("Bearer")]
        public IActionResult Put([FromBody] UsuarioVM usuarioVO)
        {

   
            if (usuarioVO == null)
                return BadRequest();

            UsuarioVM updateUsuario = _usuarioBusiness.Update(usuarioVO);
            if (updateUsuario == null)
                return NoContent();

            return new ObjectResult(updateUsuario);
        }

        [HttpDelete("{id}")]
        //[Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            _usuarioBusiness.Delete(id);
            return NoContent();
        }
    }
}
