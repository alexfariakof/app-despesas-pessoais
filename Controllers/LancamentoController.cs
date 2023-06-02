using despesas_backend_api_net_core.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class LancamentoController : Controller
    {
        private ILancamentoBusiness _lancamentoBusiness;
        public LancamentoController(ILancamentoBusiness lancamentoBusiness)
        {
            _lancamentoBusiness = lancamentoBusiness;
        }

        [HttpGet("{mesAno}/{idUsuario}")]
        //[Authorize("Bearer")]
        public IActionResult Get([FromRoute]DateTime mesAno, [FromRoute]int idUsuario)
        {
            var list = _lancamentoBusiness.FindByMesAno(mesAno, idUsuario);

            if (list == null || list.Count == 0)
                return NotFound();

            return Ok(list);
        }

        [HttpGet("Saldo/{idUsuario}")]
        //[Authorize("Bearer")]
        public IActionResult Get([FromRoute]int idUsuario)
        {
            var saldo = _lancamentoBusiness.GetSaldo(idUsuario);
                        
            return Ok(saldo.ToString("N2")); 
        }

        [HttpGet("DadosGraficoPorAno/{mesAno}/{idUsuario}")]
        //[Authorize("Bearer")]
        public IActionResult GetDadosGraficoPorAno([FromRoute]DateTime mesAno, [FromRoute]int idUsuario)
        {
            var dadosGrafico = _lancamentoBusiness.GetDadosGraficoByAno(idUsuario, mesAno);

            return Ok(dadosGrafico);
        }

    }
}