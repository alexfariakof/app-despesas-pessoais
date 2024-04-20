using Business.Abstractions;
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
    public IActionResult Post([FromBody] ControleAcessoDto controleAcessoDto)
    {
        if (String.IsNullOrEmpty(controleAcessoDto.Telefone) || String.IsNullOrWhiteSpace(controleAcessoDto.Telefone))
            return BadRequest(new { message = "Campo Telefone não pode ser em branco" });

        if (String.IsNullOrEmpty(controleAcessoDto.Email) || String.IsNullOrWhiteSpace(controleAcessoDto.Email))
            return BadRequest(new { message = "Campo Login não pode ser em branco" });

        if (!IsValidEmail(controleAcessoDto.Email))
            return BadRequest(new { message = "Email inválido!" });

        if (String.IsNullOrEmpty(controleAcessoDto.Senha) || String.IsNullOrWhiteSpace(controleAcessoDto.Senha))
            return BadRequest(new { message = "Campo Senha não pode ser em branco ou nulo" });

        if (String.IsNullOrEmpty(controleAcessoDto.ConfirmaSenha) | String.IsNullOrWhiteSpace(controleAcessoDto.ConfirmaSenha))
            return BadRequest(new { message = "Campo Confirma Senha não pode ser em branco ou nulo" });

        if (controleAcessoDto.Senha != controleAcessoDto.ConfirmaSenha)
            return BadRequest(new { message = "Senha e Confirma Senha são diferentes!" });

        ControleAcesso controleAcesso = new ControleAcesso();
        controleAcesso
            .CreateAccount(
            new Usuario().CreateUsuario(
                controleAcessoDto.Nome,
                controleAcessoDto.SobreNome,
                controleAcessoDto.Email,
                controleAcessoDto.Telefone,
                StatusUsuario.Ativo,
                PerfilUsuario.Usuario),
            controleAcessoDto.Email,
            controleAcessoDto.Senha
            );

        try
        {
            _controleAcessoBusiness.Create(controleAcesso);
            return Ok(new { message = true });
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível realizar o cadastro." });
        }
    }
    
    [AllowAnonymous]
    [HttpPost("SignIn")]
    public IActionResult SignIn([FromBody] LoginDto login)
    {
        var controleAcesso = new ControleAcesso { Login = login.Email , Senha = login.Senha };

        if (String.IsNullOrEmpty(controleAcesso.Login) || String.IsNullOrWhiteSpace(controleAcesso.Login))
            return BadRequest(new { message = "Campo Login não pode ser em branco ou nulo!" });

        if (!IsValidEmail(login.Email))
            return BadRequest(new { message = "Email inválido!" });

        if (String.IsNullOrEmpty(controleAcesso.Senha) || String.IsNullOrWhiteSpace(controleAcesso.Senha))
            return BadRequest(new { message = "Campo Senha não pode ser em branco ou nulo!" });

        var result = _controleAcessoBusiness.ValidateCredentials(new ControleAcessoDto { Email = login.Email, Senha = login.Senha });
        
        if (result == null)
            return BadRequest(new { message = "Erro ao realizar login!" });

        return new OkObjectResult(result);
    }

    [HttpPost("ChangePassword")]
    [Authorize("Bearer")]        
    public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordVM)
    {

        if (IdUsuario.Equals(2))
            return BadRequest(new { message = "A senha deste usuário não pode ser atualizada!" });

        if (String.IsNullOrEmpty(changePasswordVM.Senha) || String.IsNullOrWhiteSpace(changePasswordVM.Senha))
            return BadRequest(new { message = "Campo Senha não pode ser em branco ou nulo!" });

        if (String.IsNullOrEmpty(changePasswordVM.ConfirmaSenha) | String.IsNullOrWhiteSpace(changePasswordVM.ConfirmaSenha))
            return BadRequest(new { message = "Campo Confirma Senha não pode ser em branco ou nulo!" });

        try
        {
            _controleAcessoBusiness.ChangePassword(IdUsuario, changePasswordVM.Senha);
            return Ok(new { message = true });
        }
        catch
        {
            return BadRequest(new { message = "Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte." });
        }        
    }

    [HttpPost("RecoveryPassword")]
    [Authorize("Bearer")]
    public IActionResult RecoveryPassword([FromBody]  string email)
    {

        if (String.IsNullOrEmpty(email) || String.IsNullOrWhiteSpace(email))
            return BadRequest(new { message = "Campo Login não pode ser em branco ou nulo!" });

        if (!IsValidEmail(email))
            return BadRequest(new { message = "Email inválido!" });

        try
        {
            _controleAcessoBusiness.RecoveryPassword(email);
            return Ok(new { message = true });
        }
        catch
        {

            return BadRequest(new { message = "Email não pode ser enviado, tente novamente mais tarde." });
        }
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

    [AllowAnonymous]
    [HttpGet("refresh/{refreshToken}")]    
    public IActionResult Refresh([FromRoute] string refreshToken)
    {
        if (ModelState is { IsValid: false })
            return BadRequest("Invalid Client Request!");

        try
        {
            var result = _controleAcessoBusiness.ValidateCredentials(refreshToken);
            if (result is null)
                throw new NullReferenceException();
            
            return Ok(result);
        }
        catch
        {
           return BadRequest("Invalid Client Request!");
        }

    }

    [HttpGet("revoke")]
    [Authorize("Bearer")]
    public IActionResult Revoke()
    {
        try
        {
            _controleAcessoBusiness.RevokeToken(IdUsuario);
            return NoContent();
        }
        catch
        {
            return BadRequest("Invalid Client Request!");
        }       
    }
}