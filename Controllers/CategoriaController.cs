using despesas_backend_api_net_core.Business.Generic;
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

        public CategoriaController(IBusiness<CategoriaVM> categoriaBusiness)
        {
            _categoriaBusiness = categoriaBusiness;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(_categoriaBusiness.FindAll());
        }

        [HttpGet("GetById/{idCategoria}")]
        [Authorize("Bearer")]
        public IActionResult GetById([FromRoute] int idCategoria)
        {
            CategoriaVM _categoria = _categoriaBusiness.FindById(idCategoria);

            if (_categoria == null)
                return NotFound();

            return Ok(_categoria);
        }

        [HttpGet("GetByIdUsuario/{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult GetByIdUsuario([FromRoute] int idUsuario)
        {
            List<CategoriaVM> _categoria = _categoriaBusiness.FindByIdUsuario(idUsuario)
                                           .FindAll(c => c.IdUsuario.Equals(idUsuario));

            if (_categoria == null)
                return NotFound();

            return Ok(_categoria);
        }


        [HttpGet("GetByTipoCategoria/{idUsuario}/{tipoCategoria}")]
        [Authorize("Bearer")]
        public IActionResult GetByTipoCategoria([FromRoute] int idUsuario, [FromRoute] Domain.Entities.TipoCategoria tipoCategoria)
        {
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
            if (categoria.TipoCategoria == Domain.Entities.TipoCategoria.Todas)
                return BadRequest(new { messsage = "Nenhum tipo de Categoria foi selecionado!" });

            CategoriaVM updateCategoria = _categoriaBusiness.Update(categoria);

            if (updateCategoria == null)
                return BadRequest(new { messsage = "Erro ao atualizar categoria!" });

            return new ObjectResult(updateCategoria);
        }

        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            _categoriaBusiness.Delete(id);
            return NoContent();
        }
    }
}
