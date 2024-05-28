using Asp.Versioning;
using Business.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v2;

[ApiVersion("2")]
[Route("v{version:apiVersion}/[controller]")]
public class GraficosController : AuthController
{
    private IGraficosBusiness _graficosBusiness;
    private object labels = new();
    private object datasets = new();
    public GraficosController(IGraficosBusiness graficosBusiness)
    {
        _graficosBusiness = graficosBusiness;
    }

    [HttpGet("Bar/{ano}")]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(Dictionary<List<object>, List<string>>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    public IActionResult GetByAnoByIdUsuario([FromRoute] DateTime ano)
    {
        try
        {
            var dadosGrafico = _graficosBusiness.GetDadosGraficoByAnoByIdUsuario(IdUsuario, ano);

            datasets = new List<object> {
                new { label = "Despesas", Data = dadosGrafico?.SomatorioDespesasPorAno?.Values.ToArray(), borderColor = "rgb(255, 99, 132)", backgroundColor = "rgba(255, 99, 132, 0.5)"  },
                new { label = "Receitas", Data = dadosGrafico?.SomatorioReceitasPorAno?.Values.ToArray(), borderColor = "rgb(53, 162, 235)", backgroundColor = "rgba(53, 162, 235, 0.5)"  },
            };

            labels = new List<string> { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
            return Ok(new { datasets, labels });
        }
        catch
        {
            return BadRequest("Erro ao gerar dados do Gráfico!");
        }
    }
}
