using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SaldoController : Controller
    {
        private ISaldoBusiness _saldoBusiness;
        private string bearerToken;
        public SaldoController(ISaldoBusiness saldoBusiness)
        {
            _saldoBusiness = saldoBusiness;
            bearerToken = String.Empty;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();
            try
            {
                var saldo = _saldoBusiness.GetSaldo(_idUsuario);
                return Ok(new { message = true, saldo = saldo});
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
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();
            try
            {
                var saldo = _saldoBusiness.GetSaldoAnual(ano, _idUsuario);
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
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();
            try
            {
                var saldo = _saldoBusiness.GetSaldoByMesAno(anoMes, _idUsuario);
                return Ok(new { message = true, saldo = saldo });
            }
            catch
            {
                return BadRequest(new { message = "Erro ao gerar saldo!" });
            }
        }
    }
}
