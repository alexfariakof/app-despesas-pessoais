using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VO;
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
        public IActionResult Post([FromBody] ControleAcessoVO controleAcessoVO)
        {

            ControleAcesso controleAcesso = new ControleAcesso
            {
                Login = controleAcessoVO.Email,
                Senha = controleAcessoVO.Senha,
                Usuario = new Usuario
                {
                    Nome = controleAcessoVO.Nome,
                    SobreNome = controleAcessoVO.SobreNome,
                    Email = controleAcessoVO.Email,
                    Telefone = controleAcessoVO.Telefone,
                    StatusUsuario = StatusUsuario.Ativo

                }
            };

            if (controleAcesso == null)
                return BadRequest();

            var result = _controleAcessoBusiness.Create(controleAcesso);

            if (result)
                return  Ok(new { message = result });
            else
                return BadRequest(new { message = "Não foi possível realizar o cadastro" });
        }
        
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public IActionResult SignIn([FromBody] LoginVO login)
        {
            var controleAcesso = new ControleAcesso { Login = login.Email , Senha = login.Senha };
            if (controleAcesso == null)
                return BadRequest();

            return new ObjectResult(_controleAcessoBusiness.FindByLogin(controleAcesso));
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

