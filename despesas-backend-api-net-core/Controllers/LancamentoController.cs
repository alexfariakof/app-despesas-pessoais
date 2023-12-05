using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class LancamentoController : Controller
    {
        private ILancamentoBusiness _lancamentoBusiness;
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
                    return Ok(new { message = true, lancamentos = new List<LancamentoVM>() });

                return Ok(new { message = true, lancamentos = list });
            }
            catch
            {
                return Ok(new { message = true, lancamentos = new List<LancamentoVM>() });
            }
        }


        
    }
}