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
        //[Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(_categoriaBusiness.FindAll());
        }

        [HttpGet("{id}")]
        //[Authorize("Bearer")]
        public IActionResult Get(int id)
        {
            CategoriaVM _categoria = _categoriaBusiness.FindById(id);

            if (_categoria == null)
                return NotFound();

            return Ok(_categoria);
        }

        [HttpGet("byTipoCategoria/{idUsuario}/{tipoCategoria}")]
        //[Authorize("Bearer")]
        public IActionResult GetByTipoCategoria([FromRoute] int idUsuario, [FromRoute] Domain.Entities.TipoCategoria tipoCategoria)
        {
            var _categoria = _categoriaBusiness.FindAll()
                .FindAll(prop => prop.IdTipoCategoria.Equals(((int)tipoCategoria)) &&
                                (prop.IdUsuario.Equals(idUsuario) ||
                                 prop.IdUsuario == null ||
                                 prop.IdUsuario.Equals(0)));

  
            if (_categoria == null)
                return NotFound();

            return Ok(_categoria);
        }

        [HttpPost]
        //[Authorize("Bearer")]
        public IActionResult Post([FromBody] CategoriaVM categoria)
        {
            if (categoria == null)
                return BadRequest();

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
        //[Authorize("Bearer")]
        public IActionResult Put([FromBody] CategoriaVM categoria)
        {
            if (categoria == null)
                return BadRequest();

            CategoriaVM updateCategoria = _categoriaBusiness.Update(categoria);
            if (updateCategoria == null)
                return NoContent();

            return new ObjectResult(updateCategoria);
        }

        [HttpDelete("{id}")]
        //[Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            _categoriaBusiness.Delete(id);
            return NoContent();
        }
    }
}
