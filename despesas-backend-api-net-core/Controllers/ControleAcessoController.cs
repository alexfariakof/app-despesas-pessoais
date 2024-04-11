using Business;
using Business.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace despesas_backend_api_net_core.Controllers;

[Route("[controller]")]
[ApiController]
public class ControleAcessoController : AuthController
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
            Usuario = new Usuario().CreateUsuario(
                controleAcessoVM.Nome,
                controleAcessoVM.SobreNome,
                controleAcessoVM.Email,
                controleAcessoVM.Telefone,
                StatusUsuario.Ativo,
                PerfilUsuario.Usuario)
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

        var result = _controleAcessoBusiness.FindByLogin(controleAcesso);
        
        if (result == null)
            return BadRequest(new { message = "Erro ao realizar login!" });

        return new OkObjectResult(result);
    }

    [HttpPost("ChangePassword")]
    [Authorize("Bearer")]        
    public IActionResult ChangePassword([FromBody] ChangePasswordVM changePasswordVM)
    {

        if (IdUsuario.Equals(2))
            return BadRequest(new { message = "A senha deste usuário não pode ser atualizada!" });

        if (String.IsNullOrEmpty(changePasswordVM.Senha) || String.IsNullOrWhiteSpace(changePasswordVM.Senha))
            return BadRequest(new { message = "Campo Senha não pode ser em branco ou nulo!" });

        if (String.IsNullOrEmpty(changePasswordVM.ConfirmaSenha) | String.IsNullOrWhiteSpace(changePasswordVM.ConfirmaSenha))
            return BadRequest(new { message = "Campo Confirma Senha não pode ser em branco ou nulo!" });
        
        if (_controleAcessoBusiness.ChangePassword(IdUsuario, changePasswordVM.Senha))
                return Ok(new { message = true });

        return BadRequest(new { message = "Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte." });
    }

    [AllowAnonymous]
    [HttpPost("RecoveryPassword")]
    [Authorize("Bearer")]
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

