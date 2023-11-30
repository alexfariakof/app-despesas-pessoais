using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class LancamentoController : Controller
    {
        private ILancamentoBusiness _lancamentoBusiness;
        private object labels = null;
        private object datasets = null;
        private string bearerToken;
        public LancamentoController(ILancamentoBusiness lancamentoBusiness)
        {
            _lancamentoBusiness = lancamentoBusiness;
            bearerToken = String.Empty;
        }

        [HttpGet("{anoMes}/{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult Get([FromRoute]DateTime anoMes, [FromRoute]int idUsuario)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            try
            {
                var list = _lancamentoBusiness.FindByMesAno(anoMes, idUsuario);

                if (list == null || list.Count == 0)
                    return Ok(new { message = true, lancamento = new List<LancamentoVM>() });

                return Ok(new { message = true, lancamento = list });
            }
            catch
            {
                return Ok(new { message = true, lancamento = new List<LancamentoVM>() });
            }
        }

        [HttpGet("Saldo/{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult Get([FromRoute]int idUsuario)
        {

            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            try
            {
                var saldo = _lancamentoBusiness.GetSaldo(idUsuario).ToString("N2", new CultureInfo("pt-br"));

                return Ok(saldo);
            }
            catch
            {
                return BadRequest(new { message = "Erro ao gerar saldo!" });
            }

        }

        [HttpGet("GetDadosGraficoByAnoByIdUsuario/{anoMes}/{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult GetDadosGraficoPorAno([FromRoute]DateTime anoMes, [FromRoute]int idUsuario)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            try
            {
                var dadosGrafico = _lancamentoBusiness.GetDadosGraficoByAnoByIdUsuario(idUsuario, anoMes);

                datasets = new List<object> {
                    new { label = "Despesas", Data = dadosGrafico.SomatorioDespesasPorAno.Values.ToArray(), borderColor = "rgb(255, 99, 132)", backgroundColor = "rgba(255, 99, 132, 0.5)"  },
                    new { label = "Receitas", Data = dadosGrafico.SomatorioReceitasPorAno.Values.ToArray(), borderColor = "rgb(53, 162, 235)", backgroundColor = "rgba(53, 162, 235, 0.5)"  },
                };

                labels = new List<string> { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
                return Ok(new { datasets = datasets, labels = labels });
            }
            catch
            {
                return BadRequest(new { message = "Erro ao gerar dados do Graáfico!" });
            }
        }
    }
}