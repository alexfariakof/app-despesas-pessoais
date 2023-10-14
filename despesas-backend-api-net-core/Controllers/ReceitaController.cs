using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : Controller
    {
        private IBusiness<ReceitaVM> _receitaBusiness;
        private string bearerToken;
        public ReceitaController(IBusiness<ReceitaVM> receitaBusiness)
        {
            _receitaBusiness = receitaBusiness;
            bearerToken = String.Empty;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            return Ok(_receitaBusiness.FindAll(_idUsuario));
        }

        [HttpGet("GetById/{id}")]
        [Authorize("Bearer")]
        public IActionResult GetById([FromRoute]int id)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            try
            {
                var _receita = _receitaBusiness.FindById(id, _idUsuario);

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
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario =  bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != receita.IdUsuario)
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

        [HttpGet("GetByIdUsuario/{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult Post([FromRoute] int idUsuario)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (idUsuario == 0)
                return BadRequest(new { message = "Usuário inexistente!" });
            else
                return Ok(_receitaBusiness.FindAll(idUsuario));

        }

        [HttpPut]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] ReceitaVM receita)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != receita.IdUsuario)
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
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            ReceitaVM receita = _receitaBusiness.FindById(idReceita, _idUsuario);
            if (receita == null || _idUsuario != receita.IdUsuario)
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