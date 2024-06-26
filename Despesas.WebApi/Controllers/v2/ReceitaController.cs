﻿using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.v2;
using Business.HyperMedia.Filters;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v2;

[ApiVersion("2")]
[Route("v{version:apiVersion}/[controller]")]
public class ReceitaController : AuthController
{
    private readonly IBusinessBase<ReceitaDto, Receita> _receitaBusiness;
    public ReceitaController(IBusinessBase<ReceitaDto, Receita> receitaBusiness)
    {
        _receitaBusiness = receitaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(IList<ReceitaDto>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        try
        {
            return Ok(_receitaBusiness.FindAll(IdUsuario));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return Ok(new List<ReceitaDto>());
        }
    }

    [HttpGet("GetById/{id}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(ReceitaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            var _receita = _receitaBusiness.FindById(id, IdUsuario) ?? throw new ArgumentException("Nenhuma receita foi encontrada.");
            return Ok(_receita);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar a consulta da receita.");
        }
    }

    [HttpPost]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(ReceitaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post([FromBody] ReceitaDto receita)
    {
        try
        {
            receita.UsuarioId = IdUsuario;
            return Ok(_receitaBusiness.Create(receita));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar o cadastro da receita!");
        }
    }

    [HttpPut]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(ReceitaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put([FromBody] ReceitaDto receita)
    {
        try
        {
            receita.UsuarioId = IdUsuario;
            var updateReceita = _receitaBusiness.Update(receita) ?? throw new();
            return Ok(updateReceita);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível atualizar o cadastro da receita.");
        }
    }

    [HttpDelete("{idReceita}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Delete(int idReceita)
    {
        try
        {
            ReceitaDto receita = _receitaBusiness.FindById(idReceita, IdUsuario);
            if (receita == null || IdUsuario != receita.UsuarioId)
                throw new ArgumentException("Usuário não permitido a realizar operação!");

            return _receitaBusiness.Delete(receita) ? Ok(true) : throw new();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao excluir Receita!");
        }
    }
}