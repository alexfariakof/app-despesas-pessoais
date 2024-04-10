using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize("Bearer")]
public class GraficosController : AuthController
{
    private IGraficosBusiness _graficosBusiness;
    private object labels = null;
    private object datasets = null;
    public GraficosController(IGraficosBusiness graficosBusiness)
    {
        _graficosBusiness = graficosBusiness;
    }

    [HttpGet("Bar/{ano}")]
    [Authorize("Bearer")]
    public IActionResult GetByAnoByIdUsuario([FromRoute] DateTime ano)
    {
        try
        {
            var dadosGrafico = _graficosBusiness.GetDadosGraficoByAnoByIdUsuario(IdUsuario, ano);

            datasets = new List<object> {
                new { label = "Despesas", Data = dadosGrafico.SomatorioDespesasPorAno.Values.ToArray(), borderColor = "rgb(255, 99, 132)", backgroundColor = "rgba(255, 99, 132, 0.5)"  },
                new { label = "Receitas", Data = dadosGrafico.SomatorioReceitasPorAno.Values.ToArray(), borderColor = "rgb(53, 162, 235)", backgroundColor = "rgba(53, 162, 235, 0.5)"  },
            };

            labels = new List<string> { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
            return Ok(new { datasets = datasets, labels = labels });
        }
        catch
        {
            return BadRequest(new { message = "Erro ao gerar dados do Gráfico!" });
        }
    }
}
