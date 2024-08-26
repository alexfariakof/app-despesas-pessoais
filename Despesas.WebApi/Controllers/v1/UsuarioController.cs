using Asp.Versioning;
using Business.Dtos.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Abstractions;
using System.Text.RegularExpressions;
using Domain.Entities.ValueObjects;

namespace Despesas.WebApi.Controllers.v1;

[ApiVersion("1")]
[Route("v1/[controller]")]
[ApiController]
public class UsuarioController : AuthController
{
    private IUsuarioBusiness<UsuarioDto> _usuarioBusiness;
    private readonly IImagemPerfilUsuarioBusiness<ImagemPerfilDto, UsuarioDto> _imagemPerfilBussiness;
    public UsuarioController(IUsuarioBusiness<UsuarioDto> usuarioBusiness, IImagemPerfilUsuarioBusiness<ImagemPerfilDto, UsuarioDto> imagemPerfilBussiness)
    {
        _usuarioBusiness = usuarioBusiness;
        _imagemPerfilBussiness = imagemPerfilBussiness;
    }

    [HttpGet("GetUsuario")]
    [Authorize("Bearer", Roles = "Admin, User")]
    public IActionResult GetUsuario()
    {
        var _usuario = _usuarioBusiness.FindById(UserIdentity);
        if (_usuario == null)
            return BadRequest(new { message = "Usuário não encontrado!" });

        return Ok(_usuario);
    }

    [HttpPut("UpdateUsuario")]
    [Authorize("Bearer", Roles = "User, Admin")]
    public IActionResult Put([FromBody] UsuarioDto usuarioDto)
    {
        if (!ValidateUsuarioDto(usuarioDto, out IActionResult validationError))
            return validationError;

        var updateUsuario = _usuarioBusiness.Update(usuarioDto);
        if (updateUsuario == null)
            return BadRequest(new { message = "Usuário não encontrado!" });

        return Ok(updateUsuario);
    }

    [HttpGet]
    [Authorize("Bearer", Roles = "Admin")]
    public IActionResult Get()
    {
        var adm = _usuarioBusiness.FindById(UserIdentity);
        if (adm.PerfilUsuario != PerfilUsuario.Perfil.Admin)
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }

        return Ok(_usuarioBusiness.FindAll(UserIdentity));
    }

    [HttpPost]
    [Authorize("Bearer", Roles = "Admin")]
    public IActionResult Post([FromBody] UsuarioDto usuarioDto)
    {
        var usuario = _usuarioBusiness.FindById(UserIdentity);
        if (usuario.PerfilUsuario != PerfilUsuario.Perfil.Admin)
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }

        if (!ValidateUsuarioDto(usuarioDto, out IActionResult validationError))
            return validationError;

        return new OkObjectResult(_usuarioBusiness.Create(usuarioDto));
    }

    [HttpPut]
    [Authorize("Bearer", Roles = "Admin")]
    public IActionResult PutAdministrador([FromBody] UsuarioDto usuarioDto)
    {
        var usuario = _usuarioBusiness.FindById(UserIdentity);
        if (usuario.PerfilUsuario != PerfilUsuario.Perfil.Admin)
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }

        if (!ValidateUsuarioDto(usuarioDto, out IActionResult validationError))
            return validationError;

        var updateUsuario = _usuarioBusiness.Update(usuarioDto);
        if (updateUsuario == null)
            return BadRequest(new { message = "Usuário não encontrado!" });

        return new OkObjectResult(updateUsuario);
    }

    [HttpDelete]
    [Authorize("Bearer", Roles = "Admin")]
    public IActionResult Delete([FromBody] UsuarioDto usuarioDto)
    {
        var adm = _usuarioBusiness.FindById(UserIdentity);
        if (adm.PerfilUsuario != PerfilUsuario.Perfil.Admin)
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }

        if (_usuarioBusiness.Delete(usuarioDto))
            return new OkObjectResult(new { message = true });
        else
            return BadRequest(new { message = "Erro ao excluir Usuário!" });
    }

    [HttpGet("ImagemPerfil")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult GetImage()
    {
        var imagemPerfilUsuario = _imagemPerfilBussiness.FindAll(UserIdentity)
            .Find(prop => prop.UsuarioId.Equals(UserIdentity));

        if (imagemPerfilUsuario != null)
            return Ok(new { message = true, imagemPerfilUsuario = imagemPerfilUsuario });
        else
            return BadRequest(new { message = "Usuário não possui nenhuma imagem de perfil cadastrada!" });
    }

    [HttpPost("ImagemPerfil")]
    [Authorize("Bearer", Roles = "User")]
    public async Task<IActionResult> PostImagemPerfil(IFormFile file)
    {
        try
        {
            var imagemPerfilUsuario = await ConvertFileToImagemPerfilUsuarioDtoAsync(file, UserIdentity);
            ImagemPerfilDto? _imagemPerfilUsuario = _imagemPerfilBussiness.Create(imagemPerfilUsuario);
            if (_imagemPerfilUsuario != null)
                return Ok(new { message = true, imagemPerfilUsuario = _imagemPerfilUsuario });
            else
                return BadRequest(new { message = false, imagemPerfilUsuario = _imagemPerfilUsuario });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("ImagemPerfil")]
    [Authorize("Bearer", Roles = "User")]
    public async Task<IActionResult> PutImagemPerfil(IFormFile file)
    {
        try
        {
            ImagemPerfilDto imagemPerfilUsuario = await ConvertFileToImagemPerfilUsuarioDtoAsync(file, UserIdentity);
            imagemPerfilUsuario = _imagemPerfilBussiness.Update(imagemPerfilUsuario);
            if (imagemPerfilUsuario != null)
                return Ok(new { message = true, imagemPerfilUsuario = imagemPerfilUsuario });
            else
                return BadRequest(new { message = false });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("ImagemPerfil")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult DeleteImagemPerfil()
    {
        try
        {
            if (_imagemPerfilBussiness.Delete(UserIdentity))
                return Ok(new { message = true });
            else
                return BadRequest(new { message = false });
        }
        catch
        {
            return BadRequest(new { message = "Erro ao excluir imagem do perfil!" });
        }
    }
    
    [HttpPut("UpdateUsuarioAdmin")]
    [Authorize("Bearer", Roles = "Admin")]
    public IActionResult UpdateUsuarioAdmin([FromBody] UsuarioDto usuarioDto)
    {
        if (!ValidateUsuarioDto(usuarioDto, out IActionResult validationError))
            return validationError;

        var updateUsuario = _usuarioBusiness.Update(usuarioDto);
        if (updateUsuario == null)
            return BadRequest(new { message = "Usuário não encontrado!" });

        return Ok(updateUsuario);
    }

    [HttpDelete("DeleteUsuarioAdmin")]
    [Authorize("Bearer", Roles = "Admin")]
    public IActionResult DeleteUsuario([FromBody] UsuarioDto usuarioDto)
    {
        var adminUser = _usuarioBusiness.FindById(UserIdentity);
        if (adminUser.PerfilUsuario != PerfilUsuario.Perfil.Admin)
            return BadRequest(new { message = "Usuário não permitido para realizar essa operação!" });

        var deleted = _usuarioBusiness.Delete(usuarioDto);
        if (deleted)
            return Ok(new { message = "Usuário excluído com sucesso!" });
        else
            return BadRequest(new { message = "Erro ao excluir Usuário!" });
    }

    private bool ValidateUsuarioDto(UsuarioDto usuarioDto, out IActionResult errorResponse)
    {
        if (String.IsNullOrEmpty(usuarioDto.Telefone) || String.IsNullOrWhiteSpace(usuarioDto.Telefone))
        {
            errorResponse = BadRequest(new { message = "Campo Telefone não pode ser em branco" });
            return false;
        }

        if (String.IsNullOrEmpty(usuarioDto.Email) || String.IsNullOrWhiteSpace(usuarioDto.Email))
        {
            errorResponse = BadRequest(new { message = "Campo Login não pode ser em branco" });
            return false;
        }

        if (!IsValidEmail(usuarioDto.Email))
        {
            errorResponse = BadRequest(new { message = "Email inválido!" });
            return false;
        }

        errorResponse = BadRequest();
        return true;
    }

    private async Task<ImagemPerfilDto> ConvertFileToImagemPerfilUsuarioDtoAsync(IFormFile file, Guid idUsuario)
    {
        string fileName = $"{idUsuario}-imagem-perfil-usuario-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
        string? typeFile = Path.GetExtension(file.FileName)?.TrimStart('.');

        if (typeFile == "jpg" || typeFile == "png" || typeFile == "jpeg")
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                return new ImagemPerfilDto
                {
                    Name = fileName,
                    Type = typeFile,
                    ContentType = file.ContentType,
                    UsuarioId = UserIdentity,
                    Arquivo = memoryStream.ToArray()
                };
            }
        }
        else
        {
            throw new Exception("Apenas arquivos do tipo jpg, jpeg ou png são aceitos.");
        }
    }

    private bool IsValidEmail(string email)
    {
        const string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }
}