using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private IBusiness<CategoriaVM> _categoriaBusiness;
        private string bearerToken;

        public CategoriaController(IBusiness<CategoriaVM> categoriaBusiness)
        {
            _categoriaBusiness = categoriaBusiness;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            return Ok(_categoriaBusiness.FindAll(_idUsuario.Value));
        }

        [HttpGet("GetById/{idCategoria}")]
        [Authorize("Bearer")]
        public IActionResult GetById([FromRoute] int idCategoria)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            CategoriaVM _categoria = _categoriaBusiness.FindById(idCategoria, _idUsuario.Value);

            if (_categoria == null)
                return NotFound();

            return Ok(_categoria);
        }

        [HttpGet("GetByIdUsuario/{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult GetByIdUsuario([FromRoute] int idUsuario)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            if (_idUsuario.Value != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            List<CategoriaVM> _categoria = _categoriaBusiness.FindByIdUsuario(idUsuario)
                                           .FindAll(c => c.IdUsuario.Equals(idUsuario));

            if (_categoria.Count == 0)
                return NotFound();

            return Ok(_categoria);
        }


        [HttpGet("GetByTipoCategoria/{idUsuario}/{tipoCategoria}")]
        [Authorize("Bearer")]
        public IActionResult GetByTipoCategoria([FromRoute] int idUsuario, [FromRoute] Domain.Entities.TipoCategoria tipoCategoria)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            if (_idUsuario.Value != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (tipoCategoria == Domain.Entities.TipoCategoria.Todas)
            {
                var _categoria = _categoriaBusiness.FindByIdUsuario(idUsuario)
                                 .FindAll(prop => prop.IdUsuario.Equals(idUsuario));
                return Ok(_categoria);
            }
            else
            {
                var _categoria = _categoriaBusiness.FindByIdUsuario(idUsuario)
                                .FindAll(prop => prop.IdTipoCategoria.Equals(((int)tipoCategoria)));
                return Ok(_categoria);
            }           
        }

        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] CategoriaVM categoria)
        {

            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            if (_idUsuario.Value != categoria.IdUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (categoria.IdTipoCategoria == (int)Domain.Entities.TipoCategoria.Todas)
                return BadRequest(new { message = "Nenhum tipo de Categoria foi selecionado!"});

            try
            {
                return new ObjectResult(new { message = true, categoria = _categoriaBusiness.Create(categoria) });
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
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            if (_idUsuario.Value != categoria.IdUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (categoria.TipoCategoria == Domain.Entities.TipoCategoria.Todas)
                return BadRequest(new { messsage = "Nenhum tipo de Categoria foi selecionado!" });

            CategoriaVM updateCategoria = _categoriaBusiness.Update(categoria);

            if (updateCategoria == null)
                return BadRequest(new { messsage = "Erro ao atualizar categoria!" });

            return new ObjectResult(updateCategoria);
        }

        [HttpDelete]
        [Authorize("Bearer")]
        public IActionResult Delete([FromBody] CategoriaVM categoria)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            if (_idUsuario.Value != categoria.IdUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            if (_categoriaBusiness.Delete(categoria.IdUsuario))
            {
                return new ObjectResult(new { Message = true });
            }

            return new ObjectResult(new { Message = false });
        }
    }
}