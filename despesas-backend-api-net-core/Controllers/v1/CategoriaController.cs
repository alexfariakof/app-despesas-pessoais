using Asp.Versioning;
<<<<<<< HEAD
using Business.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Generic;
=======
using Business.Dtos.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Business.Dtos.Core;
using Business.Abstractions.Generic;
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER

namespace despesas_backend_api_net_core.Controllers.v1;

[ApiVersion("1")]
[Route("v1/[controller]")]
public class CategoriaController : AuthController
{
<<<<<<< HEAD
    private IBusiness<CategoriaDto> _categoriaBusiness;
    public CategoriaController(IBusiness<CategoriaDto> categoriaBusiness)
=======
    private IBusiness<CategoriaDto, Categoria> _categoriaBusiness;
    public CategoriaController(IBusiness<CategoriaDto, Categoria> categoriaBusiness)
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
    {
        _categoriaBusiness = categoriaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer")]
    public IActionResult Get()
    {
<<<<<<< HEAD
        List<CategoriaDto> _categoria = _categoriaBusiness.FindAll(IdUsuario);
=======
        var _categoria = _categoriaBusiness.FindAll(IdUsuario);
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
        return Ok(_categoria);
    }

    [HttpGet("GetById/{idCategoria}")]
    [Authorize("Bearer")]
    public IActionResult GetById([FromRoute] int idCategoria)
    {

<<<<<<< HEAD
        CategoriaDto _categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
=======
        var _categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
        return Ok(_categoria);
    }

    [HttpGet("GetByTipoCategoria/{tipoCategoria}")]
    [Authorize("Bearer")]
<<<<<<< HEAD
    public IActionResult GetByTipoCategoria([FromRoute] Domain.Entities.TipoCategoria tipoCategoria)
    {
        if (tipoCategoria == Domain.Entities.TipoCategoria.Todas)
        {
            var _categoria = _categoriaBusiness.FindAll(IdUsuario)
                             .FindAll(prop => prop.IdUsuario.Equals(IdUsuario));
=======
    public IActionResult GetByTipoCategoria([FromRoute] TipoCategoriaDto tipoCategoria)
    {
        if (tipoCategoria == TipoCategoriaDto.Todas)
        {
            var _categoria = _categoriaBusiness.FindAll(IdUsuario)
                             .FindAll(prop => prop.UsuarioId.Equals(IdUsuario));
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
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

<<<<<<< HEAD
        if (categoria.IdTipoCategoria == (int)Domain.Entities.TipoCategoria.Todas)
=======
        if (categoria.IdTipoCategoria == (int)TipoCategoriaDto.Todas)
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
            return BadRequest(new { message = "Nenhum tipo de Categoria foi selecionado!"});

        try
        {
<<<<<<< HEAD
            categoria.IdUsuario = IdUsuario;
=======
            categoria.UsuarioId = IdUsuario;
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
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

<<<<<<< HEAD
        if (categoria.TipoCategoria == Domain.Entities.TipoCategoria.Todas)
            return BadRequest(new { message = "Nenhum tipo de Categoria foi selecionado!" });

        categoria.IdUsuario = IdUsuario;
        CategoriaDto updateCategoria = _categoriaBusiness.Update(categoria);
=======
        if (categoria.IdTipoCategoria == (int)TipoCategoriaDto.Todas)
            return BadRequest(new { message = "Nenhum tipo de Categoria foi selecionado!" });

        categoria.UsuarioId = IdUsuario;
        var updateCategoria = _categoriaBusiness.Update(categoria);
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER

        if (updateCategoria == null)
            return BadRequest(new { message = "Erro ao atualizar categoria!" });

        return Ok(new { message = true, categoria = updateCategoria });
    }

    [HttpDelete("{idCategoria}")]
    [Authorize("Bearer")]
    public IActionResult Delete(int idCategoria)
    {
<<<<<<< HEAD
        CategoriaDto categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
        if (categoria == null || IdUsuario != categoria.IdUsuario)
=======
        var categoria = _categoriaBusiness.FindById(idCategoria, IdUsuario);
        if (categoria == null || IdUsuario != categoria.UsuarioId)
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
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