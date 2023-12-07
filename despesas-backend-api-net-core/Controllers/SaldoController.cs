using despesas_backend_api_net_core.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SaldoController : AuthController
    {
        private ISaldoBusiness _saldoBusiness;
        public SaldoController(ISaldoBusiness saldoBusiness)
        {
            _saldoBusiness = saldoBusiness;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
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
