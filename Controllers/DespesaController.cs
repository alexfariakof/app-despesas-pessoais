using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : Controller
    {
        private IBusiness<DespesaVM> _despesaBusiness;

        public DespesaController(IBusiness<DespesaVM> despesaBusiness)
        {
            _despesaBusiness = despesaBusiness;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(_despesaBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]
        public IActionResult Get(int id)
        {
            try
            {
                var _despesa = _despesaBusiness.FindById(id);

                if (_despesa == null)
                    return Ok( new { message = "Nenhuma despesa foi encontrada."});

                return new ObjectResult(new { message = true, despesa = _despesa });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível realizar a consulta da despesa." });
            }
        }

        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] DespesaVM despesa)
        {
            if (despesa == null)
                return BadRequest();
            try
            {
                return new ObjectResult(new { message = true, despesa = _despesaBusiness.Create(despesa) });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível realizar o cadastro da despesa."});
            }
        }

        [HttpPut]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] DespesaVM despesa)
        {
            if (despesa == null)
                return BadRequest();

            var updateDespesa = _despesaBusiness.Update(despesa);
            if (updateDespesa == null)
                return BadRequest(new { message = "Não foi possível atualizar o cadastro da despesa." });

            return new ObjectResult(new { message = true, despesa = updateDespesa });
        }

        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            _despesaBusiness.Delete(id);
            return NoContent();
        }
    }
}
