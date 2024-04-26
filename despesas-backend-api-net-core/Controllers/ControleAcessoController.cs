﻿using Business.Abstractions;
using Business.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        try
        {
            _controleAcessoBusiness.Create(controleAcessoDto);
            return Ok(true);
        }
        catch  (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar o cadastro.");
        }
    }
    
    [AllowAnonymous]
    [HttpPost("SignIn")]
    [ProducesResponseType((200), Type = typeof(AuthenticationDto))]
    [ProducesResponseType((400), Type = typeof(string))]
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
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult RecoveryPassword([FromBody] string email)
    {
        try
        {
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
    [ProducesResponseType((200), Type = typeof(AuthenticationDto))]
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