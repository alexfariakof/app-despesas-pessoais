using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.Core;
using Business.Dtos.v2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v2;

[ApiVersion("2")]
[Route("v{version:apiVersion}/[controller]")]
public class ControleAcessoController : AuthController
{
    private IControleAcessoBusiness<ControleAcessoDto, LoginDto> _controleAcessoBusiness;
    public ControleAcessoController(IControleAcessoBusiness<ControleAcessoDto, LoginDto> controleAcessoBusiness)
    {
        _controleAcessoBusiness = controleAcessoBusiness;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    public IActionResult Post([FromBody] ControleAcessoDto controleAcessoDto)
    {
        try
        {
            _controleAcessoBusiness.Create(controleAcessoDto);
            return Ok(true);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar o cadastro.");
        }
    }

    [AllowAnonymous]
    [HttpPost("SignIn")]
    [ProducesResponseType(200, Type = typeof(AuthenticationDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    public IActionResult SignIn([FromBody] LoginDto login)
    {
        try
        {
            var result = _controleAcessoBusiness.ValidateCredentials(login);
            if (result == null)
                throw new Exception();

            return new OkObjectResult(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar o login do usuário.");
        }
    }


    [HttpPost("ChangePassword")]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordVM)
    {
        try
        {
            if (IdUsuario.Equals(2))
                throw new ArgumentException("A senha deste usuário não pode ser atualizada!");

            _controleAcessoBusiness.ChangePassword(IdUsuario, changePasswordVM.Senha);
            return Ok(true);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte.");
        }
    }

    [HttpPost("RecoveryPassword")]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    public IActionResult RecoveryPassword([FromBody] string email)
    {
        try
        {
            if (IdUsuario.Equals(2))
                throw new Exception();

            _controleAcessoBusiness.RecoveryPassword(email);
            return Ok(true);
        }
        catch
        {
            return NoContent();
        }
    }

    [AllowAnonymous]
    [HttpGet("refresh/{refreshToken}")]
    [ProducesResponseType(200, Type = typeof(AuthenticationDto))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Refresh([FromRoute] string refreshToken)
    {
        try
        {
            var result = _controleAcessoBusiness.ValidateCredentials(refreshToken);
            if (result is null)
                throw new NullReferenceException();

            return Ok(result);
        }
        catch
        {
            return NoContent();
        }
    }
}