using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.v2;
using Business.HyperMedia.Filters;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers.v2;

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
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(IList<DespesaDto>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        try
        {
            return Ok(_despesaBusiness.FindAll(IdUsuario));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return Ok(new List<DespesaDto>());
        }
    }

    [HttpGet("GetById/{id}")]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(DespesaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get([FromRoute] int id)
    {
        try
        {
            var _despesa = _despesaBusiness.FindById(id, IdUsuario);
            if (_despesa == null)
                return BadRequest("Nenhuma despesa foi encontrada.");

            return new OkObjectResult(_despesa);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar a consulta da despesa.");
        }
    }

    [HttpPost]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(DespesaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post([FromBody] DespesaDto despesa)
    {
        try
        {
            despesa.UsuarioId = IdUsuario;
            return new OkObjectResult(_despesaBusiness.Create(despesa));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar o cadastro da despesa.");
        }
    }

    [HttpPut]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(DespesaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put([FromBody] DespesaDto despesa)
    {
        try
        {
            despesa.UsuarioId = IdUsuario;
            var updateDespesa = _despesaBusiness.Update(despesa);
            if (updateDespesa == null)
                throw new Exception();

            return new OkObjectResult(updateDespesa);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível atualizar o cadastro da despesa.");
        }
    }

    [HttpDelete("{idDespesa}")]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Delete(int idDespesa)
    {
        try
        {
            DespesaDto despesa = _despesaBusiness.FindById(idDespesa, IdUsuario);
            if (despesa == null || IdUsuario != despesa.UsuarioId)
            {
                return BadRequest("Usuário não permitido a realizar operação!");
            }

            if (_despesaBusiness.Delete(despesa))
                return new OkObjectResult(true);
            else
                throw new Exception();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao excluir Despesa!");
        }
    }
}