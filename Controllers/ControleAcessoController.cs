using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControleAcessoController : Controller
    {
        private IControleAcessoBusiness _controleAcessoBusiness;

        public ControleAcessoController(IControleAcessoBusiness controleAcessoBusiness)
        {
            _controleAcessoBusiness = controleAcessoBusiness;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] ControleAcessoVM controleAcessoVM)
        {

            ControleAcesso controleAcesso = new ControleAcesso
            {
                Login = controleAcessoVM.Email,
                Senha = controleAcessoVM.Senha,
                Usuario = new Usuario
                {
                    Nome = controleAcessoVM.Nome,
                    SobreNome = controleAcessoVM.SobreNome,
                    Email = controleAcessoVM.Email,
                    Telefone = controleAcessoVM.Telefone,
                    StatusUsuario = StatusUsuario.Ativo

                }
            };

            if (string.IsNullOrEmpty(controleAcesso.Usuario.Email) || string.IsNullOrWhiteSpace(controleAcesso.Usuario.Email))
                return BadRequest("Email não pode ser nulo ou conter espaços em branco!");

            if (string.IsNullOrEmpty(controleAcesso.Senha) || string.IsNullOrWhiteSpace(controleAcesso.Senha))
                return BadRequest("Senha não pode ser nula ou conter espaços em branco!");


            var result = _controleAcessoBusiness.Create(controleAcesso);

            if (result)
                return  Ok(new { message = result });
            else
                return BadRequest(new { message = "Não foi possível realizar o cadastro.Usuário já deve estar cadastrado ou email já cadastrado!" });
        }
        
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public IActionResult SignIn([FromBody] LoginVM login)
        {
            var controleAcesso = new ControleAcesso { Login = login.Email , Senha = login.Senha };
            if (String.IsNullOrEmpty(controleAcesso.Login) || String.IsNullOrWhiteSpace(controleAcesso.Login))
                return BadRequest("Campo Login não pode ser em branco");

            if (String.IsNullOrEmpty(controleAcesso.Senha) || String.IsNullOrWhiteSpace(controleAcesso.Senha))
                return BadRequest("Campo Senha não pode ser em branco");

            return new ObjectResult(_controleAcessoBusiness.FindByLogin(controleAcesso));
        }


        [HttpPost("ChangePassword")]
        //[Authorize("Bearer")]
        
        public IActionResult ChangePassword([FromBody] LoginVM login)
        {
            if (_controleAcessoBusiness.ChangePassword(login.IdUsuario.ToInteger(), login.Senha))
                    return Ok(new { message = true });

            return BadRequest(new { message = "Erro ao trocar senha tente novamente mis tarde ou entre em contato com nosso suporte." });
        }


        [AllowAnonymous]
        [HttpPost("RecoveryPassword")]
        public IActionResult RecoveryPassword([FromBody]  string email)
        {
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrEmpty(email))
                if (_controleAcessoBusiness.RecoveryPassword(email))
                    return Ok(new { message = true });
                else
                    return Ok(new { message = "Email não pode ser enviado, tente novamente mais tarde."});

            return BadRequest(new { message = "Não foi possível enviar o email, tente novamente mis tarde ou entre em contato com nosso suporte." });
        }

    }
}

