using Asp.Versioning;
using Business.Dtos.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Generic;
using Domain.Entities;

namespace despesas_backend_api_net_core.Controllers.v1;

[ApiVersion("1")]
[Route("v1/[controller]")]
public class CategoriaController : AuthController
{
    private IBusiness<CategoriaDto, Categoria> _categoriaBusiness;
    public CategoriaController(IBusiness<CategoriaDto, Categoria> categoriaBusiness)
    {
        _categoriaBusiness = categoriaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer")]
    public IActionResult Get()
    {
        var _categoria = _categoriaBusiness.FindAll(IdUsuario);
        return Ok(_categoria);
    }

    [HttpGet("GetById/{idCategoria}")]
    [Authorize("Bearer")]
    public IActionResult GetById([FromRoute] int idCategoria)
    {

        var _categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
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
    public IActionResult Post([FromBody] CategoriaDto categoria)
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
    public IActionResult Put([FromBody] CategoriaDto categoria)
    {

        if (categoria.TipoCategoria == Domain.Entities.TipoCategoria.Todas)
            return BadRequest(new { message = "Nenhum tipo de Categoria foi selecionado!" });

        categoria.IdUsuario = IdUsuario;
        var updateCategoria = _categoriaBusiness.Update(categoria);

        if (updateCategoria == null)
            return BadRequest(new { message = "Erro ao atualizar categoria!" });

        return Ok(new { message = true, categoria = updateCategoria });
    }

    [HttpDelete("{idCategoria}")]
    [Authorize("Bearer")]
    public IActionResult Delete(int idCategoria)
    {
        var categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
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