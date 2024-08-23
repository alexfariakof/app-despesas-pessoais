﻿using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.v2;
using Business.HyperMedia.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v2;

[ApiVersion("2")]
[Route("v{version:apiVersion}/[controller]")]
public class UsuarioController : AuthController
{
    private readonly IUsuarioBusiness<UsuarioDto> _usuarioBusiness;
    private readonly IImagemPerfilUsuarioBusiness<ImagemPerfilDto, UsuarioDto> _imagemPerfilBussiness;
    
    public UsuarioController(IUsuarioBusiness<UsuarioDto> usuarioBusiness, IImagemPerfilUsuarioBusiness<ImagemPerfilDto, UsuarioDto> imagemPerfilBussiness)
    {
        _usuarioBusiness = usuarioBusiness;
        _imagemPerfilBussiness = imagemPerfilBussiness;
    }
    
    [HttpGet("GetUsuario")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(UsuarioDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetUsuarioById()
    {
        try
        {
            var _usuario = _usuarioBusiness.FindById(IdUsuario);
            if (_usuario == null) throw new();
            return Ok(_usuario);
        }
        catch(Exception ex) 
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Usuário não encontrado!");
        }
    }

    [HttpPut("UpdateUsuario")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(UsuarioDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult UpdateUsuario([FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            usuarioDto.UsuarioId = IdUsuario;            
            return Ok(_usuarioBusiness.Update(usuarioDto));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao atualizar Usuário!");
        }
    }

    [HttpGet]
    [Authorize("Bearer", Roles = "Admin")]
    [ProducesResponseType(200, Type = typeof(List<UsuarioDto>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        try
        {
            var usuarios = _usuarioBusiness.FindAll(IdUsuario);
            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Não foi possível realizar a consulta de usuários.");
        }       
    }

    [HttpPost]
    [Authorize("Bearer", Roles = "Admin")]
    [ProducesResponseType(200, Type = typeof(UsuarioDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post([FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            usuarioDto.UsuarioId = IdUsuario;
            return Ok(_usuarioBusiness.Create(usuarioDto));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao cadastrar Usuário!");
        }
    }

    [HttpPut]
    [Authorize("Bearer", Roles = "Admin")]
    [ProducesResponseType(200, Type = typeof(UsuarioDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put([FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            usuarioDto.UsuarioId = IdUsuario;
            return Ok(_usuarioBusiness.Update(usuarioDto));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao atualizar Usuário!");
        }
    }

    [HttpDelete]
    [Authorize("Bearer", Roles = "Admin")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Delete([FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            if (_usuarioBusiness.Delete(usuarioDto))
                return  Ok(true);

            throw new ArgumentException("Não foi possivél excluir este usuário.");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao excluir Usuário!");
        }
    }

    [HttpGet("ImagemPerfil")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(ImagemPerfilDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetImagemPerfil()
    {
        try
        {

            var imagemPerfilUsuario = _imagemPerfilBussiness.FindAll(IdUsuario).Find(prop => prop.UsuarioId.Equals(IdUsuario));

            if (imagemPerfilUsuario != null)
                return Ok(imagemPerfilUsuario);
            else
                return Ok(new ImagemPerfilDto());
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Usuário não possui nenhuma imagem de perfil cadastrada!");
        }
    }

    [HttpPost("ImagemPerfil")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(ImagemPerfilDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public async Task<IActionResult> PostImagemPerfil(IFormFile file)
    {
        try
        {
            ImagemPerfilDto imagemPerfilUsuario = await ConvertFileToImagemPerfilUsuarioDtoAsync(file, IdUsuario);
            var _imagemPerfilUsuario = _imagemPerfilBussiness.Create(imagemPerfilUsuario);

            if (_imagemPerfilUsuario != null)
                return Ok(_imagemPerfilUsuario);
            else
                throw new();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao incluir imagem de perfil!");
        }
    }

    [HttpPut("ImagemPerfil")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(ImagemPerfilDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public async Task<IActionResult> PutImagemPerfil(IFormFile file)
    {
        try
        {
            ImagemPerfilDto imagemPerfilUsuario = await ConvertFileToImagemPerfilUsuarioDtoAsync(file, IdUsuario);
            imagemPerfilUsuario = _imagemPerfilBussiness.Update(imagemPerfilUsuario);
            if (imagemPerfilUsuario != null)
                return Ok(imagemPerfilUsuario);
            else
                throw new();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao atualizar imagem de perfil!");
        }
    }

    [HttpDelete("ImagemPerfil")]
    [Authorize("Bearer", Roles = "User")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult DeleteImagemPerfil()
    {
        try
        {
            if (_imagemPerfilBussiness.Delete(IdUsuario))
                return Ok(true);
            else
                throw new();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao excluir imagem do perfil!");
        }
    }

    private async Task<ImagemPerfilDto> ConvertFileToImagemPerfilUsuarioDtoAsync(IFormFile file, int idUsuario)
    {
        string fileName = idUsuario + "-imagem-perfil-usuario-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        string typeFile = "";
        int posicaoUltimoPontoNoArquivo = file.FileName.LastIndexOf('.');
        if (posicaoUltimoPontoNoArquivo >= 0 && posicaoUltimoPontoNoArquivo < file.FileName.Length - 1)
            typeFile = file.FileName.Substring(posicaoUltimoPontoNoArquivo + 1);

        if (typeFile == "jpg" || typeFile == "png" || typeFile == "jpeg")
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                ImagemPerfilDto imagemPerfilUsuario = new ImagemPerfilDto
                {

                    Name = fileName,
                    Type = typeFile,
                    ContentType = file.ContentType,
                    UsuarioId = IdUsuario,
                    Arquivo = memoryStream.GetBuffer()
                };
                return imagemPerfilUsuario;
            }
        }
        else
            throw new ArgumentException("Apenas arquivos do tipo jpg, jpeg ou png são aceitos.");
    }
}