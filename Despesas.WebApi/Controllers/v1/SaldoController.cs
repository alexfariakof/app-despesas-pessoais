using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Abstractions;

namespace Despesas.WebApi.Controllers.v1;

public class SaldoController : AuthController
{
    private ISaldoBusiness _saldoBusiness;
    public SaldoController(ISaldoBusiness saldoBusiness)
    {
        _saldoBusiness = saldoBusiness;
    }

    [HttpGet]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Get()
    {
        try
        {
            var saldo = _saldoBusiness.GetSaldo(UserIdentity);
            return Ok(new { message = true, saldo = saldo });
        }
        catch
        {
            return BadRequest(new { message = "Erro ao gerar saldo!" });
        }
    }

    [HttpGet("ByAno/{ano}")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult GetSaldoByAno([FromRoute] DateTime ano)
    {
        try
        {
            var saldo = _saldoBusiness.GetSaldoAnual(ano, UserIdentity);
            return Ok(new { message = true, saldo = saldo });
        }
        catch
        {
            return BadRequest(new { message = "Erro ao gerar saldo!" });
        }
    }

    [HttpGet("ByMesAno/{anoMes}")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult GetSaldoByMesAno([FromRoute] DateTime anoMes)
    {
        try
        {
            var saldo = _saldoBusiness.GetSaldoByMesAno(anoMes, UserIdentity);
            return Ok(new { message = true, saldo = saldo });
        }
        catch
        {
            return BadRequest(new { message = "Erro ao gerar saldo!" });
        }
    }
}
