using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.Core;
using Business.Dtos.v2;
using Business.HyperMedia.Filters;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v2;

[ApiVersion("2")]
[Route("{version}/[controller]")]
public class CategoriaController : AuthController
{
    private readonly IBusinessBase<CategoriaDto, Categoria> _categoriaBusiness;
    public CategoriaController(IBusinessBase<CategoriaDto, Categoria> categoriaBusiness)
    {
        _categoriaBusiness = categoriaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(List<CategoriaDto>))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        try
        {
            IList<CategoriaDto> _categoria = _categoriaBusiness.FindAll(UserIdentity);
            return Ok(_categoria);
        }
        catch
        {
            return Ok(new List<CategoriaDto>());
        }
    }

    [HttpGet("GetById/{idCategoria}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(CategoriaDto))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetById([FromRoute] Guid idCategoria)
    {
        try
        {
            CategoriaDto _categoria = _categoriaBusiness.FindById(idCategoria, UserIdentity);
            return Ok(_categoria);
        }
        catch
        {
            return Ok(new CategoriaDto());
        }
    }

    [HttpGet("GetByTipoCategoria/{tipoCategoria}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(List<CategoriaDto>))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetByTipoCategoria([FromRoute] TipoCategoriaDto tipoCategoria)
    {
        if (tipoCategoria == TipoCategoriaDto.Todas)
        {
            var _categoria = _categoriaBusiness.FindAll(UserIdentity).Where(prop => prop.UsuarioId.Equals(UserIdentity)).ToList();
            return Ok(_categoria);
        }
        else
        {
            var _categoria = _categoriaBusiness.FindAll(UserIdentity).Where(prop => prop.IdTipoCategoria.Equals((TipoCategoriaDto)tipoCategoria)).ToList();
            return Ok(_categoria);
        }
    }

    [HttpPost]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(CategoriaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post([FromBody] CategoriaDto categoria)
    {
        try
        {
            if (categoria.IdTipoCategoria == (int)TipoCategoriaDto.Todas)
                throw new ArgumentException("Nenhum tipo de Categoria foi selecionado!");

            categoria.UsuarioId = UserIdentity;
            return Ok(_categoriaBusiness.Create(categoria));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar o cadastro de uma nova categoria, tente mais tarde ou entre em contato com o suporte.");
        }
    }

    [HttpPut]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(CategoriaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put([FromBody] CategoriaDto categoria)
    {
        try
        {
            if (categoria.IdTipoCategoria == TipoCategoriaDto.Todas)
                throw new ArgumentException("Nenhum tipo de Categoria foi selecionado!");

            categoria.UsuarioId = UserIdentity;
            CategoriaDto updateCategoria = _categoriaBusiness.Update(categoria) ?? throw new();
            return Ok(updateCategoria);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao atualizar categoria!");
        }
    }

    [HttpDelete("{idCategoria}")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Delete(Guid idCategoria)
    {
        try
        {
            CategoriaDto categoria = _categoriaBusiness.FindById(idCategoria, UserIdentity);
            if (categoria == null || UserIdentity != categoria.UsuarioId)
                throw new ArgumentException("Usuário não permitido a realizar operação!");

            if (_categoriaBusiness.Delete(categoria))
                return Ok(true);

            return Ok(false);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao deletar categoria!");
        }
    }
}