using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.v2;
using Despesas.WebApi.Controllers.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v2;

public class LancamentoController : AuthController
{
    private ILancamentoBusiness<LancamentoDto> _lancamentoBusiness;
    public LancamentoController(ILancamentoBusiness<LancamentoDto> lancamentoBusiness)
    {
        _lancamentoBusiness = lancamentoBusiness;
    }

    [HttpGet("{anoMes}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(List<LancamentoDto>))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public IActionResult Get([FromRoute] DateTime anoMes)
    {
        try
        {
            var list = _lancamentoBusiness.FindByMesAno(anoMes, UserIdentity);
            if (list == null || list.Count == 0)
                return Ok(new List<LancamentoDto>());

            return Ok(list);
        }
        catch
        {
            return Ok(new List<LancamentoDto>());
        }
    }
}