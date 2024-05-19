using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Abstractions;
<<<<<<< HEAD
using Business.Dtos;
=======
using Business.Dtos.v1;
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER

namespace despesas_backend_api_net_core.Controllers.v1;

[ApiVersion("1")]
[Route("v1/[controller]")]
[ApiController]
public class LancamentoController : AuthController
{
<<<<<<< HEAD
    private ILancamentoBusiness _lancamentoBusiness;
    public LancamentoController(ILancamentoBusiness lancamentoBusiness)
=======
    private ILancamentoBusiness<LancamentoDto> _lancamentoBusiness;
    public LancamentoController(ILancamentoBusiness<LancamentoDto> lancamentoBusiness)
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
    {
        _lancamentoBusiness = lancamentoBusiness;
    }

    [HttpGet("{anoMes}")]
    [Authorize("Bearer")]
    public IActionResult Get([FromRoute]DateTime anoMes)
    {
        try
        {
            var list = _lancamentoBusiness.FindByMesAno(anoMes, IdUsuario);

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