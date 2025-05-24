using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Abstractions;
using Business.Dtos.v1;

namespace Despesas.WebApi.Controllers.v1;

public class LancamentoController : AuthController
{
    private ILancamentoBusiness<LancamentoDto> _lancamentoBusiness;
    public LancamentoController(ILancamentoBusiness<LancamentoDto> lancamentoBusiness)
    {
        _lancamentoBusiness = lancamentoBusiness;
    }

    [HttpGet("{anoMes}")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Get([FromRoute] DateTime anoMes)
    {
        try
        {
            var list = _lancamentoBusiness.FindByMesAno(anoMes, UserIdentity);

            if (list == null || list.Count == 0)
                return Ok(new { message = true, lancamentos = new List<LancamentoDto>() });

            return Ok(new { message = true, lancamentos = list });
        }
        catch
        {
            return Ok(new { message = true, lancamentos = new List<LancamentoDto>() });
        }
    }

}