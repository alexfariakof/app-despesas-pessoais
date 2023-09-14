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
    public class ControleAcessoController : Controller
    {
        private IControleAcessoBusiness _controleAcessoBusiness;
        private string bearerToken;
        public ControleAcessoController(IControleAcessoBusiness controleAcessoBusiness)
        {
            _controleAcessoBusiness = controleAcessoBusiness;
            bearerToken = String.Empty;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] ControleAcessoVM controleAcessoVM)
        {
            if (String.IsNullOrEmpty(controleAcessoVM.Telefone) || String.IsNullOrWhiteSpace(controleAcessoVM.Telefone))
                return BadRequest(new { message = "Campo Telefone não pode ser em branco" });

            if (String.IsNullOrEmpty(controleAcessoVM.Email) || String.IsNullOrWhiteSpace(controleAcessoVM.Email))
                return BadRequest(new { message = "Campo Login não pode ser em branco" });

            if (!IsValidEmail(controleAcessoVM.Email))
                return BadRequest(new { message = "Email inválido!" });

            if (String.IsNullOrEmpty(controleAcessoVM.Senha) || String.IsNullOrWhiteSpace(controleAcessoVM.Senha))
                return BadRequest(new { message = "Campo Senha não pode ser em branco ou nulo" });

            if (String.IsNullOrEmpty(controleAcessoVM.ConfirmaSenha) | String.IsNullOrWhiteSpace(controleAcessoVM.ConfirmaSenha))
                return BadRequest(new { message = "Campo Confirma Senha não pode ser em branco ou nulo" });

            if (controleAcessoVM.Senha != controleAcessoVM.ConfirmaSenha)
                return BadRequest(new { message = "Senha e Confirma Senha são diferentes!" });

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

            var result = _controleAcessoBusiness.Create(controleAcesso);

            if (result)
                return  Ok(new { message = result });
            else
                return BadRequest(new { message = "Não foi possível realizar o cadastro." });
        }
        
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public IActionResult SignIn([FromBody] LoginVM login)
        {
            var controleAcesso = new ControleAcesso { Login = login.Email , Senha = login.Senha };

            if (String.IsNullOrEmpty(controleAcesso.Login) || String.IsNullOrWhiteSpace(controleAcesso.Login))
                return BadRequest(new { message = "Campo Login não pode ser em branco ou nulo!" });

            if (!IsValidEmail(login.Email))
                return BadRequest(new { message = "Email inválido!" });

            if (String.IsNullOrEmpty(controleAcesso.Senha) || String.IsNullOrWhiteSpace(controleAcesso.Senha))
                return BadRequest(new { message = "Campo Senha não pode ser em branco ou nulo!" });

            return new ObjectResult(_controleAcessoBusiness.FindByLogin(controleAcesso));
        }


        [HttpPost("ChangePassword")]
        [Authorize("Bearer")]        
        public IActionResult ChangePassword([FromBody] LoginVM login)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != login.IdUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (login.IdUsuario.Equals(2))
                return BadRequest(new { message = "A senha deste usuário não pode ser atualizada!" });

            if (String.IsNullOrEmpty(login.Senha) || String.IsNullOrWhiteSpace(login.Senha))
                return BadRequest(new { message = "Campo Senha não pode ser em branco ou nulo!" });

            if (String.IsNullOrEmpty(login.ConfirmaSenha) | String.IsNullOrWhiteSpace(login.ConfirmaSenha))
                return BadRequest(new { message = "Campo Confirma Senha não pode ser em branco ou nulo!" });

            if (_controleAcessoBusiness.ChangePassword(login.IdUsuario.ToInteger(), login.Senha))
                    return Ok(new { message = true });

            return BadRequest(new { message = "Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte." });
        }

        [AllowAnonymous]
        [HttpPost("RecoveryPassword")]
        public IActionResult RecoveryPassword([FromBody]  string email)
        {

            if (String.IsNullOrEmpty(email) || String.IsNullOrWhiteSpace(email))
                return BadRequest(new { message = "Campo Login não pode ser em branco ou nulo!" });

            if (!IsValidEmail(email))
                return BadRequest(new { message = "Email inválido!" });

            if (_controleAcessoBusiness.RecoveryPassword(email))
                return Ok(new { message = true });
            else
                return BadRequest(new { message = "Email não pode ser enviado, tente novamente mais tarde." });
        }
        private bool IsValidEmail(string email)
        {
            if (email.Length > 256)
            {
                return false;
            }

            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}

