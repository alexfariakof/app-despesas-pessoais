using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.v2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers.v2;

[ApiVersion("2")]
[Route("v{version:apiVersion}/[controller]")]
public class LancamentoController : AuthController
{
    private ILancamentoBusiness<LancamentoDto> _lancamentoBusiness;
    public LancamentoController(ILancamentoBusiness<LancamentoDto> lancamentoBusiness)
    {
        _lancamentoBusiness = lancamentoBusiness;
    }

    [HttpGet("{anoMes}")]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(List<LancamentoDto>))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    public IActionResult Get([FromRoute] DateTime anoMes)
    {
        try
        {
            var list = _lancamentoBusiness.FindByMesAno(anoMes, IdUsuario);
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