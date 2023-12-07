using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class LancamentoController : AuthController
    {
        private ILancamentoBusiness _lancamentoBusiness;
        public LancamentoController(ILancamentoBusiness lancamentoBusiness)
        {
            _lancamentoBusiness = lancamentoBusiness;
        }

        [HttpGet("{anoMes}/{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult Get([FromRoute]DateTime anoMes, [FromRoute]int idUsuario)
        {
            if (IdUsuario != idUsuario)
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