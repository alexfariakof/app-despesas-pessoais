using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Text.RegularExpressions;

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

        [HttpGet("{IdUsuario}")]
        //[Authorize("Bearer")]
        public IActionResult Get([FromRoute]int IdUsuario)
        {
            var usuario = _usuarioBusiness.FindById(IdUsuario);
            if (usuario.PerfilUsuario != PerfilUsuario.Administrador)
                return Ok(new List<UsuarioVM>());

            return Ok(_usuarioBusiness.FindAll());
        }

        //[Authorize("Bearer")]
        [HttpPost("GetById")]
        public IActionResult Post([FromBody] int idUsuario)
        {
            
            UsuarioVM _usuario = _usuarioBusiness.FindById(idUsuario);
            if (_usuario == null)
                return BadRequest(new { message ="Usuário não encontrado!" });

            return Ok(_usuario);
        }

        [HttpPost]
        //[Authorize("Bearer")]
        public IActionResult Post([FromBody] UsuarioVM usuarioVM)
        {
            if (String.IsNullOrEmpty(usuarioVM.Telefone) || String.IsNullOrWhiteSpace(usuarioVM.Telefone))
                return BadRequest("Campo Telefone não pode ser em branco");

            if (String.IsNullOrEmpty(usuarioVM.Email) || String.IsNullOrWhiteSpace(usuarioVM.Email))
                return BadRequest("Campo Login não pode ser em branco");

            if (!IsValidEmail(usuarioVM.Email))
                return BadRequest(new { message = "Email inválido!" });

            return new ObjectResult(_usuarioBusiness.Create(usuarioVM));
        }

        [HttpPut]
        //[Authorize("Bearer")]
        public IActionResult Put([FromBody] UsuarioVM usuarioVM)
        {
            if (String.IsNullOrEmpty(usuarioVM.Telefone) || String.IsNullOrWhiteSpace(usuarioVM.Telefone))
                return BadRequest("Campo Telefone não pode ser em branco");

            if (String.IsNullOrEmpty(usuarioVM.Email) || String.IsNullOrWhiteSpace(usuarioVM.Email))
                return BadRequest("Campo Login não pode ser em branco");

            if (!IsValidEmail(usuarioVM.Email))
                return BadRequest(new { message = "Email inválido!" });

            UsuarioVM updateUsuario = _usuarioBusiness.Update(usuarioVM);
            if (updateUsuario == null)
                return  BadRequest(new { message = "Usuário não encontrado!" });

            return new ObjectResult(updateUsuario);
        }

        [HttpDelete("{id}")]
        //[Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            UsuarioVM _usuario = _usuarioBusiness.FindById(id);
            if (_usuario.PerfilUsuario != PerfilUsuario.Administrador)
                return BadRequest(new { message = "Usuário não possui permissão para exectar deleção!" });
                
            _usuarioBusiness.Delete(id);
            return NoContent();
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
