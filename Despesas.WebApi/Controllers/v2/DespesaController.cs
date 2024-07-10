using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.v2;
using Business.HyperMedia.Filters;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v2;

[ApiVersion("2")]
[Route("v{version:apiVersion}/[controller]")]
public class DespesaController : AuthController
{
    private readonly IBusinessBase<DespesaDto, Despesa> _despesaBusiness;
    public DespesaController(IBusinessBase<DespesaDto, Despesa> despesaBusiness)
    {
        _despesaBusiness = despesaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(IList<DespesaDto>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        try
        {
            return Ok(_despesaBusiness.FindAll(UserIdentity));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return Ok(new List<DespesaDto>());
        }
    }

    [HttpGet("GetById/{id}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(DespesaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get([FromRoute] int id)
    {
        try
        {
            var _despesa = _despesaBusiness.FindById(id, UserIdentity) ?? throw new ArgumentException("Nenhuma despesa foi encontrada.");
            return Ok(_despesa);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar a consulta da despesa.");
        }
    }

    [HttpPost]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(DespesaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post([FromBody] DespesaDto despesa)
    {
        try
        {
            despesa.UsuarioId = UserIdentity;
            return Ok(_despesaBusiness.Create(despesa));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar o cadastro da despesa.");
        }
    }

    [HttpPut]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(DespesaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put([FromBody] DespesaDto despesa)
    {
        try
        {
            despesa.UsuarioId = UserIdentity;
            var updateDespesa = _despesaBusiness.Update(despesa) ?? throw new();
            return Ok(updateDespesa);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível atualizar o cadastro da despesa.");
        }
    }

    [HttpDelete("{idDespesa}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Delete(int idDespesa)
    {
        try
        {
            DespesaDto despesa = _despesaBusiness.FindById(idDespesa, UserIdentity);
            if (despesa == null || UserIdentity != despesa.UsuarioId)
                throw new ArgumentException("Usuário não permitido a realizar operação!");
            
            return _despesaBusiness.Delete(despesa) ? Ok(true) : throw new();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao excluir Despesa!");
        }
    }
}