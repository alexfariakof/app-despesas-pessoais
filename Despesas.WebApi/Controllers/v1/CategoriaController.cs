using Asp.Versioning;
using Business.Dtos.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Business.Dtos.Core;
using Business.Abstractions.Generic;

namespace Despesas.WebApi.Controllers.v1;

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
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(List<CategoriaDto>))]
    public IActionResult Get()
    {
        var _categoria = _categoriaBusiness.FindAll(IdUsuario);
        return Ok(_categoria);
    }

    [HttpGet("GetById/{idCategoria}")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult GetById([FromRoute] int idCategoria)
    {

        var _categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
        return Ok(_categoria);
    }

    [HttpGet("GetByTipoCategoria/{tipoCategoria}")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult GetByTipoCategoria([FromRoute] TipoCategoriaDto tipoCategoria)
    {
        if (tipoCategoria == TipoCategoriaDto.Todas)
        {
            var _categoria = _categoriaBusiness.FindAll(IdUsuario)
                             .FindAll(prop => prop.UsuarioId.Equals(IdUsuario));
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
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Post([FromBody] CategoriaDto categoria)
    {

        if (categoria.IdTipoCategoria == (int)TipoCategoriaDto.Todas)
            return BadRequest(new { message = "Nenhum tipo de Categoria foi selecionado!" });

        try
        {
            categoria.UsuarioId = IdUsuario;
            return Ok(new { message = true, categoria = _categoriaBusiness.Create(categoria) });
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível realizar o cadastro de uma nova categoria, tente mais tarde ou entre em contato com o suporte." });
        }
    }

    [HttpPut]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Put([FromBody] CategoriaDto categoria)
    {

        if (categoria.IdTipoCategoria == (int)TipoCategoriaDto.Todas)
            return BadRequest(new { message = "Nenhum tipo de Categoria foi selecionado!" });

        categoria.UsuarioId = IdUsuario;
        var updateCategoria = _categoriaBusiness.Update(categoria);

        if (updateCategoria == null)
            return BadRequest(new { message = "Erro ao atualizar categoria!" });

        return Ok(new { message = true, categoria = updateCategoria });
    }

    [HttpDelete("{idCategoria}")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Delete(int idCategoria)
    {
        var categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
        if (categoria == null || IdUsuario != categoria.UsuarioId)
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