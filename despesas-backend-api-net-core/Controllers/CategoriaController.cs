using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CategoriaController : Controller
    {
        private IBusiness<CategoriaVM> _categoriaBusiness;
        private string bearerToken;
        public CategoriaController(IBusiness<CategoriaVM> categoriaBusiness)
        {
            _categoriaBusiness = categoriaBusiness;
            bearerToken = String.Empty;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            List<CategoriaVM> _categoria = _categoriaBusiness.FindAll(_idUsuario);

            if (_categoria.Count == 0)
                return NotFound();

            return Ok(_categoria);
        }
    
        [HttpGet("GetById/{idCategoria}")]
        [Authorize("Bearer")]
        public IActionResult GetById([FromRoute] int idCategoria)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            CategoriaVM _categoria = _categoriaBusiness.FindById(idCategoria, _idUsuario);

            if (_categoria == null)
                return NotFound();

            return Ok(_categoria);
        }

        [HttpGet("GetByTipoCategoria/{tipoCategoria}")]
        [Authorize("Bearer")]
        public IActionResult GetByTipoCategoria([FromRoute] Domain.Entities.TipoCategoria tipoCategoria)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario =  bearerToken.getIdUsuarioFromToken();

            if (tipoCategoria == Domain.Entities.TipoCategoria.Todas)
            {
                var _categoria = _categoriaBusiness.FindAll(_idUsuario)
                                 .FindAll(prop => prop.IdUsuario.Equals(_idUsuario));
                return Ok(_categoria);
            }
            else
            {
                var _categoria = _categoriaBusiness.FindAll(_idUsuario)
                                .FindAll(prop => prop.IdTipoCategoria.Equals(((int)tipoCategoria)));
                return Ok(_categoria);
            }           
        }

        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] CategoriaVM categoria)
        {

            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != categoria.IdUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (categoria.IdTipoCategoria == (int)Domain.Entities.TipoCategoria.Todas)
                return BadRequest(new { message = "Nenhum tipo de Categoria foi selecionado!"});

            try
            {
                return Ok(new { message = true, categoria = _categoriaBusiness.Create(categoria) });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível realizar o cadastro de uma nova categoria, tente mais tarde ou entre em contato com o suporte." });
            }
        }

        [HttpPut]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] CategoriaVM categoria)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != categoria.IdUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (categoria.TipoCategoria == Domain.Entities.TipoCategoria.Todas)
                return BadRequest(new { message = "Nenhum tipo de Categoria foi selecionado!" });

            CategoriaVM updateCategoria = _categoriaBusiness.Update(categoria);

            if (updateCategoria == null)
                return BadRequest(new { message = "Erro ao atualizar categoria!" });

            return Ok(new { message = true, categoria = updateCategoria });
        }

        [HttpDelete("{idCategoria}")]
        [Authorize("Bearer")]
        public IActionResult Delete(int idCategoria)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            CategoriaVM categoria = _categoriaBusiness.FindById(idCategoria, _idUsuario);
            if (categoria == null || _idUsuario != categoria.IdUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (_categoriaBusiness.Delete(categoria))
            {
                return Ok(new { message = true });
            }

            return new BadRequestObjectResult(new { message = false });
        }        
    }
}