using Business.Abstractions;
using Business.Dtos;
using Business.HyperMedia.Filters;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriaController : AuthController
{
    private readonly BusinessBase<CategoriaDto, Categoria> _categoriaBusiness;
    public CategoriaController(BusinessBase<CategoriaDto, Categoria> categoriaBusiness)
    {
        _categoriaBusiness = categoriaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(List<CategoriaDto>))]
    [ProducesResponseType((401), Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        try
        {
            IList<CategoriaDto> _categoria = _categoriaBusiness.FindAll(IdUsuario).Result;
            return Ok(_categoria);
        }
        catch
        {
            return Ok(new List<CategoriaDto>());
        }
    }

    [HttpGet("GetById/{idCategoria}")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(CategoriaDto))]
    [ProducesResponseType((401), Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetById([FromRoute] int idCategoria)
    {
        try
        {
            CategoriaDto _categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
            return Ok(_categoria);
        }
        catch
        {
            return Ok(new CategoriaDto());
        }
    }

    [HttpGet("GetByTipoCategoria/{tipoCategoria}")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(List<CategoriaDto>))]
    [ProducesResponseType((401), Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetByTipoCategoria([FromRoute] TipoCategoria tipoCategoria)
    {
        if (tipoCategoria == TipoCategoria.Todas)
        {
            var _categoria = _categoriaBusiness.FindAll(IdUsuario).Result.Where(prop => prop.IdUsuario.Equals(IdUsuario)).ToList();
            return Ok(_categoria);
        }
        else
        {
            var _categoria = _categoriaBusiness.FindAll(IdUsuario).Result.Where(prop => prop.IdTipoCategoria.Equals(((int)tipoCategoria))).ToList();
            return Ok(_categoria);
        }
    }

    [HttpPost]
    [Authorize("Bearer")]
    [ProducesResponseType(typeof(CategoriaDto), StatusCodes.Status200OK)]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((401), Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post([FromBody] CategoriaDto categoria)
    {
        if (categoria.IdTipoCategoria == (int)TipoCategoria.Todas)
            return BadRequest("Nenhum tipo de Categoria foi selecionado!");

        try
        {
            categoria.IdUsuario = IdUsuario;
            return Ok(_categoriaBusiness.Create(categoria));
        }
        catch
        {
            return BadRequest("Não foi possível realizar o cadastro de uma nova categoria, tente mais tarde ou entre em contato com o suporte.");
        }
    }

    [HttpPut]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(CategoriaDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((401), Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put([FromBody] CategoriaDto categoria)
    {
        if (categoria.TipoCategoria == TipoCategoria.Todas)
            return BadRequest("Nenhum tipo de Categoria foi selecionado!");

        try
        {
            categoria.IdUsuario = IdUsuario;
            CategoriaDto updateCategoria = _categoriaBusiness.Update(categoria);
            if (updateCategoria == null) throw new Exception();
            return Ok(updateCategoria);
        }
        catch
        {
            return BadRequest("Erro ao atualizar categoria!");
        }
    }

    [HttpDelete("{idCategoria}")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((401), Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Delete(int idCategoria)
    {
        try
        {
            CategoriaDto categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
            if (categoria == null || IdUsuario != categoria.IdUsuario)
                return BadRequest("Usuário não permitido a realizar operação!");

            if (_categoriaBusiness.Delete(categoria))
                return Ok(true);

            return new OkObjectResult(false);
        }
        catch
        {
            return BadRequest("Erro ao deletar categoria!");
        }
    }        
}