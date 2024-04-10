using Business.Generic;
using Domain.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : AuthController
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
            List<CategoriaVM> _categoria = _categoriaBusiness.FindAll(IdUsuario);
            return Ok(_categoria);
        }
    
        [HttpGet("GetById/{idCategoria}")]
        [Authorize("Bearer")]
        public IActionResult GetById([FromRoute] int idCategoria)
        {

            CategoriaVM _categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
            return Ok(_categoria);
        }

        [HttpGet("GetByTipoCategoria/{tipoCategoria}")]
        [Authorize("Bearer")]
        public IActionResult GetByTipoCategoria([FromRoute] Domain.Entities.TipoCategoria tipoCategoria)
        {
            if (tipoCategoria == Domain.Entities.TipoCategoria.Todas)
            {
                var _categoria = _categoriaBusiness.FindAll(IdUsuario)
                                 .FindAll(prop => prop.IdUsuario.Equals(IdUsuario));
                return Ok(_categoria);
            }
            else
            {
                var _categoria = _categoriaBusiness.FindAll(IdUsuario)
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
                categoria.IdUsuario = IdUsuario;
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

            if (categoria.TipoCategoria == Domain.Entities.TipoCategoria.Todas)
                return BadRequest(new { message = "Nenhum tipo de Categoria foi selecionado!" });

            categoria.IdUsuario = IdUsuario;
            CategoriaVM updateCategoria = _categoriaBusiness.Update(categoria);

            if (updateCategoria == null)
                return BadRequest(new { message = "Erro ao atualizar categoria!" });

            return Ok(new { message = true, categoria = updateCategoria });
        }

        [HttpDelete("{idCategoria}")]
        [Authorize("Bearer")]
        public IActionResult Delete(int idCategoria)
        {
            CategoriaVM categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
            if (categoria == null || IdUsuario != categoria.IdUsuario)
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