﻿using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UsuarioController : AuthController
    {
        private IUsuarioBusiness _usuarioBusiness;
        public UsuarioController(IUsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        [HttpGet("{IdUsuario}")]
        [Authorize("Bearer")]
        public IActionResult Get([FromRoute]int idUsuario)
        {
            if (IdUsuario != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            var usuario = _usuarioBusiness.FindById(IdUsuario);
            if (usuario.PerfilUsuario != PerfilUsuario.Administrador)
                return Ok(new List<UsuarioVM>());

            return Ok(_usuarioBusiness.FindAll(IdUsuario));
        }
                
        [HttpGet("GetUsuario")]
        [Authorize("Bearer")]
        public IActionResult GetUsuario()
        {
            UsuarioVM _usuario = _usuarioBusiness.FindById(IdUsuario);
            if (_usuario == null)
                return BadRequest(new { message ="Usuário não encontrado!" });

            return Ok(_usuario);
        }

        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] UsuarioVM usuarioVM)
        {
            if (IdUsuario != usuarioVM.Id)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (String.IsNullOrEmpty(usuarioVM.Telefone) || String.IsNullOrWhiteSpace(usuarioVM.Telefone))
                return BadRequest(new { message = "Campo Telefone não pode ser em branco" });

            if (String.IsNullOrEmpty(usuarioVM.Email) || String.IsNullOrWhiteSpace(usuarioVM.Email))
                return BadRequest(new { message = "Campo Login não pode ser em branco" });

            if (!IsValidEmail(usuarioVM.Email))
                return BadRequest(new { message = "Email inválido!" });

            return new OkObjectResult(_usuarioBusiness.Create(usuarioVM));
        }

        [HttpPut]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] UsuarioVM usuarioVM)
        {
            if (IdUsuario != usuarioVM.Id)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (String.IsNullOrEmpty(usuarioVM.Telefone) || String.IsNullOrWhiteSpace(usuarioVM.Telefone))
                return BadRequest(new { message = "Campo Telefone não pode ser em branco" });

            if (String.IsNullOrEmpty(usuarioVM.Email) || String.IsNullOrWhiteSpace(usuarioVM.Email))
                return BadRequest(new { message = "Campo Login não pode ser em branco" });

            if (!IsValidEmail(usuarioVM.Email))
                return BadRequest(new { message = "Email inválido!" });

            UsuarioVM updateUsuario = _usuarioBusiness.Update(usuarioVM);
            if (updateUsuario == null)
                return  BadRequest(new { message = "Usuário não encontrado!" });

            return new OkObjectResult(updateUsuario);
        }

        [HttpDelete("{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult Delete([FromBody] UsuarioVM usuarioVM, int idUsuario)
        {
            if (IdUsuario != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            UsuarioVM _usuario = _usuarioBusiness.FindById(idUsuario);
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
