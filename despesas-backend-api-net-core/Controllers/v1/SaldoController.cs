using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Abstractions;

namespace despesas_backend_api_net_core.Controllers.v1;

[ApiVersion("1")]
[Route("v1/[controller]")]
[ApiController]
public class SaldoController : AuthController
{
    private ISaldoBusiness _saldoBusiness;
    public SaldoController(ISaldoBusiness saldoBusiness)
    {
        _saldoBusiness = saldoBusiness;
    }

    [HttpGet]
    [Authorize("Bearer")]
    public IActionResult Get()
    {
        try
        {
            var saldo = _saldoBusiness.GetSaldo(IdUsuario);
            return Ok(new { message = true, saldo = saldo });
        }
        catch
        {
            return BadRequest(new { message = "Erro ao gerar saldo!" });
        }
    }

    [HttpGet("ByAno/{ano}")]
    [Authorize("Bearer")]
    public IActionResult GetSaldoByAno([FromRoute] DateTime ano)
    {
        try
        {
            var saldo = _saldoBusiness.GetSaldoAnual(ano, IdUsuario);
            return Ok(new { message = true, saldo = saldo });
        }
        catch
        {
            return BadRequest(new { message = "Erro ao gerar saldo!" });
        }
    }

    [HttpGet("ByMesAno/{anoMes}")]
    [Authorize("Bearer")]
    public IActionResult GetSaldoByMesAno([FromRoute] DateTime anoMes)
    {
        try
        {
            var saldo = _saldoBusiness.GetSaldoByMesAno(anoMes, IdUsuario);
            return Ok(new { message = true, saldo = saldo });
        }
        catch
        {
            return BadRequest(new { message = "Erro ao gerar saldo!" });
        }
    }
}
