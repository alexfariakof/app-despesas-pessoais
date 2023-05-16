using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : Controller
    {
        private IBusiness<Despesa> _despesaBusiness;

        public DespesaController(IBusiness<Despesa> despesaBusiness)
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
                Despesa _despesa = _despesaBusiness.FindById(id);

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
        public IActionResult Post([FromBody] Despesa despesa)
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
        public IActionResult Put([FromBody] Despesa despesa)
        {
            if (despesa == null)
                return BadRequest();

            Despesa updateDespesa = _despesaBusiness.Update(despesa);
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
