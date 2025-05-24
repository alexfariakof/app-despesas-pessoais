﻿using Business.Abstractions;
using Business.Dtos.Core;
using Business.Dtos.v2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v2;

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
            var result = _controleAcessoBusiness.ValidateCredentials(login) ?? throw new();
            return Ok(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar o login do usuário.");
        }
    }


    [HttpPost("ChangePassword")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordVM)
    {
        try
        {
            _controleAcessoBusiness.ChangePassword(UserIdentity, changePasswordVM.Senha ?? "");
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
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
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

    [HttpGet("refresh/{refreshToken}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(AuthenticationDto))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(403)]
    public IActionResult Refresh([FromRoute] string refreshToken)
    {
        try
        {
            var result = _controleAcessoBusiness.ValidateCredentials(refreshToken) ?? throw new();
            return Ok(result);
        }
        catch
        {
            return NoContent();
        }
    }
}