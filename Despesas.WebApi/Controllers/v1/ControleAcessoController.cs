using Business.Abstractions;
using Business.Dtos.v1;
using Despesas.WebApi.Controllers.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Despesas.WebApi.Controllers.v1;
[Route("v1/[controller]")]

public class ControleAcessoController : BaseAuthController
{
    private IControleAcessoBusiness<ControleAcessoDto, LoginDto> _controleAcessoBusiness;
    public ControleAcessoController(IControleAcessoBusiness<ControleAcessoDto, LoginDto> controleAcessoBusiness)
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

        try
        {
            _controleAcessoBusiness.Create(controleAcessoDto);
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
        try
        {
            if (String.IsNullOrEmpty(login.Email) || String.IsNullOrWhiteSpace(login.Email))
                return BadRequest(new { message = "Campo Login não pode ser em branco ou nulo!" });

            if (!IsValidEmail(login.Email))
                return BadRequest(new { message = "Email inválido!" });

            if (String.IsNullOrEmpty(login.Senha) || String.IsNullOrWhiteSpace(login.Senha))
                return BadRequest(new { message = "Campo Senha não pode ser em branco ou nulo!" });

            var result = _controleAcessoBusiness.ValidateCredentials(login);
            if (result == null) throw new NullReferenceException();
            return new OkObjectResult(result);
        }
        catch
        {
            return BadRequest(new { message = "Erro ao realizar login!" });
        }
    }

    [HttpPost("ChangePassword")]
    [Authorize("Bearer", Roles = "Admin, User")]
    public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {

        if (UserIdentity.Equals(2))
            return BadRequest(new { message = "A senha deste usuário não pode ser atualizada!" });

        if (String.IsNullOrEmpty(changePasswordDto.Senha) || String.IsNullOrWhiteSpace(changePasswordDto.Senha))
            return BadRequest(new { message = "Campo Senha não pode ser em branco ou nulo!" });

        if (String.IsNullOrEmpty(changePasswordDto.ConfirmaSenha) | String.IsNullOrWhiteSpace(changePasswordDto.ConfirmaSenha))
            return BadRequest(new { message = "Campo Confirma Senha não pode ser em branco ou nulo!" });
        try
        {
            _controleAcessoBusiness.ChangePassword(UserIdentity, changePasswordDto.Senha);
            return Ok(new { message = true });
        }
        catch
        {

            return BadRequest(new { message = "Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte." });
        }
    }

    [AllowAnonymous]
    [HttpPost("RecoveryPassword")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult RecoveryPassword([FromBody] string email)
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

    private static bool IsValidEmail(string email)
    {
        if (email.Length > 256) return false;
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
}