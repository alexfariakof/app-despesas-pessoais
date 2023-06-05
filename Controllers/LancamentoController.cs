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

        [HttpGet("{anoMes}/{idUsuario}")]
        //[Authorize("Bearer")]
        public IActionResult Get([FromRoute]DateTime anoMes, [FromRoute]int idUsuario)
        {
            var list = _lancamentoBusiness.FindByMesAno(anoMes, idUsuario);

            if (list == null || list.Count == 0)
                return BadRequest("Nenhum Lançamento foi encontrado!");

            return Ok(list);
        }

        [HttpGet("Saldo/{idUsuario}")]
        //[Authorize("Bearer")]
        public IActionResult Get([FromRoute]int idUsuario)
        {
            var saldo = _lancamentoBusiness.GetSaldo(idUsuario);
                        
            return Ok(saldo.ToString("N2")); 
        }

        [HttpGet("DadosGraficoPorAno/{anoMes}/{idUsuario}")]
        //[Authorize("Bearer")]
        public IActionResult GetDadosGraficoPorAno([FromRoute]DateTime anoMes, [FromRoute]int idUsuario)
        {
            var dadosGrafico = _lancamentoBusiness.GetDadosGraficoByAno(idUsuario, anoMes);

            return Ok(dadosGrafico);
        }

    }
}