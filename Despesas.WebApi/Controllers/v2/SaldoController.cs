using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.v2;
using Business.HyperMedia.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v2;

[ApiVersion("2")]
[Route("{version}/[controller]")]
public class SaldoController : AuthController
{
    private ISaldoBusiness _saldoBusiness;
    public SaldoController(ISaldoBusiness saldoBusiness)
    {
        _saldoBusiness = saldoBusiness;
    }

    [HttpGet]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(SaldoDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        try
        {
            var saldo = _saldoBusiness.GetSaldo(UserIdentity);
            return Ok(saldo);
        }
        catch
        {
            return BadRequest("Erro ao gerar saldo!");
        }
    }

    [HttpGet("ByAno/{ano}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(SaldoDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetSaldoByAno([FromRoute] DateTime ano)
    {
        try
        {
            var saldo = _saldoBusiness.GetSaldoAnual(ano, UserIdentity);
            return Ok(saldo);
        }
        catch
        {
            return BadRequest("Erro ao gerar saldo!");
        }
    }

    [HttpGet("ByMesAno/{anoMes}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(SaldoDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetSaldoByMesAno([FromRoute] DateTime anoMes)
    {
        try
        {
            var saldo = _saldoBusiness.GetSaldoByMesAno(anoMes, UserIdentity);
            return Ok(saldo);
        }
        catch
        {
            return BadRequest("Erro ao gerar saldo!");
        }
    }
}
