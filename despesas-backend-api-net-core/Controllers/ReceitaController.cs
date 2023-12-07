using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ReceitaController : AuthController
    {
        private IBusiness<ReceitaVM> _receitaBusiness;
        public ReceitaController(IBusiness<ReceitaVM> receitaBusiness)
        {
            _receitaBusiness = receitaBusiness;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        { 
            return Ok(_receitaBusiness.FindAll(IdUsuario));
        }

        [HttpGet("GetById/{id}")]
        [Authorize("Bearer")]
        public IActionResult GetById([FromRoute]int id)
        {
            try
            {
                var _receita = _receitaBusiness.FindById(id, IdUsuario);

                if (_receita == null)
                    return BadRequest(new { message = "Nenhuma receita foi encontrada." });

                return new OkObjectResult(new { message = true, receita = _receita });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível realizar a consulta da receita." });
            }
        }

        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] ReceitaVM receita)
        {
            if (IdUsuario != receita.IdUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            try
            {
                return new OkObjectResult(new { message = true, receita = _receitaBusiness.Create(receita) });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível realizar o cadastro da receita!" });
            }            
        }

        [HttpPut]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] ReceitaVM receita)
        {
            if (IdUsuario != receita.IdUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            var updateReceita = _receitaBusiness.Update(receita);

            if (updateReceita == null)
                return BadRequest(new { message = "Não foi possível atualizar o cadastro da receita." });

            return new OkObjectResult(new { message = true, receita = updateReceita });
        }

        [HttpDelete("{idReceita}")]
        [Authorize("Bearer")]
        public IActionResult Delete(int idReceita)
        {
            ReceitaVM receita = _receitaBusiness.FindById(idReceita, IdUsuario);
            if (receita == null || IdUsuario != receita.IdUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (_receitaBusiness.Delete(receita))
                return new OkObjectResult(new { message = true });
            else
                return BadRequest(new { message = "Erro ao excluir Receita!" });            
            
        }
    }
}