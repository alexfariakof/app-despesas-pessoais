using despesas_backend_api_net_core.Interfaces;
using despesas_backend_api_net_core.Services;
using despesas_backend_api_net_core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private ICategoriaViewModelService _categoriaViewModelService;
        public CategoriaController(ICategoriaViewModelService categoriaViewModelService)
        {
            _categoriaViewModelService = categoriaViewModelService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoriaViewModelService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CategoriaViewModel _categoria = _categoriaViewModelService.Get(id);

            if (_categoria == null)
                return NotFound();

            return Ok(_categoria);
        }

        [HttpGet("byTipoCategoria/{idUsuario}/{idTipoCategoria}")]
        public IActionResult GetByTipoCategoria([FromRoute] int idUsuario, [FromRoute] int idTipoCategoria)
        {
            var _categoria = _categoriaViewModelService.GetAll()
                .FindAll(prop => prop.IdTipoCategoria.Equals(idTipoCategoria) &&
                                (prop.IdUsuario.Equals(idUsuario) ||
                                 prop.IdUsuario == null ||
                                 prop.IdUsuario.Equals(0)));

            if (_categoria == null)
                return NotFound();

            return Ok(_categoria);
        }

        [HttpPost]
        //[Authorize("Bearer")]
        public IActionResult Post([FromBody] CategoriaViewModel categoria)
        {
            if (categoria == null)
                return BadRequest();

            try
            {
                return new ObjectResult(new { message = true, categoria = _categoriaViewModelService.Insert(categoria) });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível realizar o cadastro de uma nova categoria, tente mais tarde ou entre em contato com o suporte." });
            }
        }

        [HttpPut]
        //[Authorize("Bearer")]
        public IActionResult Put([FromBody] CategoriaViewModel categoria)
        {
            if (categoria == null)
                return BadRequest();

            CategoriaViewModel updateCategoria = _categoriaViewModelService.Update(categoria);
            if (updateCategoria == null)
                return NoContent();

            return new ObjectResult(updateCategoria);
        }

        [HttpDelete("{id}")]
        //[Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            _categoriaViewModelService.Delete(id);
            return NoContent();
        }
    }
}
