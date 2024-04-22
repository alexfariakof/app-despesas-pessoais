using Business.Abstractions;
using Business.Dtos;
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
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult Post([FromBody] ControleAcessoDto controleAcessoDto)
    {
        if (String.IsNullOrEmpty(controleAcessoDto.Telefone) || String.IsNullOrWhiteSpace(controleAcessoDto.Telefone))
            return BadRequest("Campo Telefone não pode ser em branco");

        if (String.IsNullOrEmpty(controleAcessoDto.Email) || String.IsNullOrWhiteSpace(controleAcessoDto.Email))
            return BadRequest("Campo Login não pode ser em branco");

        if (!IsValidEmail(controleAcessoDto.Email))
            return BadRequest("Email inválido!");

        if (String.IsNullOrEmpty(controleAcessoDto.Senha) || String.IsNullOrWhiteSpace(controleAcessoDto.Senha))
            return BadRequest("Campo Senha não pode ser em branco ou nulo");

        if (String.IsNullOrEmpty(controleAcessoDto.ConfirmaSenha) | String.IsNullOrWhiteSpace(controleAcessoDto.ConfirmaSenha))
            return BadRequest("Campo Confirma Senha não pode ser em branco ou nulo");

        if (controleAcessoDto.Senha != controleAcessoDto.ConfirmaSenha)
            return BadRequest("Senha e Confirma Senha são diferentes!");

        try
        {
            _controleAcessoBusiness.Create(controleAcessoDto);
            return Ok(true);
        }
        catch
        {
            return BadRequest("Não foi possível realizar o cadastro.");
        }
    }
    
    [AllowAnonymous]
    [HttpPost("SignIn")]
    [ProducesResponseType((200), Type = typeof(AuthenticationDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult SignIn([FromBody] LoginDto login)
    {
        if (String.IsNullOrEmpty(login.Email) || String.IsNullOrWhiteSpace(login.Email))
            return BadRequest("Campo Login não pode ser em branco ou nulo!");

        if (!IsValidEmail(login.Email))
            return BadRequest("Email inválido!");

        if (String.IsNullOrEmpty(login.Senha) || String.IsNullOrWhiteSpace(login.Senha))
            return BadRequest("Campo Senha não pode ser em branco ou nulo!");

        var result = _controleAcessoBusiness.ValidateCredentials(new ControleAcessoDto { Email = login.Email, Senha = login.Senha });
        
        if (result == null)
            return BadRequest("Erro ao realizar login!");

        return new OkObjectResult(result);
    }

    [HttpPost("ChangePassword")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((401), Type = typeof(UnauthorizedResult))]
    public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordVM)
    {
        if (IdUsuario.Equals(2))
            return BadRequest("A senha deste usuário não pode ser atualizada!");

        if (String.IsNullOrEmpty(changePasswordVM.Senha) || String.IsNullOrWhiteSpace(changePasswordVM.Senha))
            return BadRequest("Campo Senha não pode ser em branco ou nulo!");

        if (String.IsNullOrEmpty(changePasswordVM.ConfirmaSenha) | String.IsNullOrWhiteSpace(changePasswordVM.ConfirmaSenha))
            return BadRequest("Campo Confirma Senha não pode ser em branco ou nulo!");

        try
        {
            _controleAcessoBusiness.ChangePassword(IdUsuario, changePasswordVM.Senha);
            return Ok(true);
        }
        catch
        {
            return BadRequest("Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte.");
        }        
    }

    [HttpPost("RecoveryPassword")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((401), Type = typeof(UnauthorizedResult))]
    public IActionResult RecoveryPassword([FromBody]  string email)
    {
        if (String.IsNullOrEmpty(email) || String.IsNullOrWhiteSpace(email))
            return BadRequest("Campo Login não pode ser em branco ou nulo!");

        if (!IsValidEmail(email))
            return BadRequest("Email inválido!");

        try
        {
            _controleAcessoBusiness.RecoveryPassword(email);
            return Ok(true);
        }
        catch
        {
            return BadRequest("Email não pode ser enviado, tente novamente mais tarde.");
        }
    }

    [AllowAnonymous]
    [HttpGet("refresh/{refreshToken}")]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult Refresh([FromRoute] string refreshToken)
    {
        if (ModelState is { IsValid: false })
            return BadRequest("Invalid Client Request.");

        try
        {
            var result = _controleAcessoBusiness.ValidateCredentials(refreshToken);
            if (result is null)
                throw new NullReferenceException();
            
            return Ok(result);
        }
        catch
        {
           return BadRequest("Invalid Client Request.");
        }
    }

    [HttpGet("revoke")]
    [Authorize("Bearer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult Revoke()
    {
        try
        {
            _controleAcessoBusiness.RevokeToken(IdUsuario);
            return NoContent();
        }
        catch
        {
            return BadRequest("Invalid Client Request.");
        }       
    }

    private bool IsValidEmail(string email)
    {
        if (email.Length > 256) return false;

        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
}