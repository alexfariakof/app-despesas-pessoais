using Business.Abstractions;
using Business.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace despesas_backend_api_net_core.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize("Bearer")]
public class UsuarioController : AuthController
{
    private IUsuarioBusiness _usuarioBusiness;
    private readonly IImagemPerfilUsuarioBusiness _imagemPerfilBussiness;
    public UsuarioController(IUsuarioBusiness usuarioBusiness, IImagemPerfilUsuarioBusiness imagemPerfilBussiness)
    {
        _usuarioBusiness = usuarioBusiness;
        _imagemPerfilBussiness = imagemPerfilBussiness;
    }

    [HttpGet]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(IList<UsuarioDto>))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Get()
    {
        try
        {
            var adm = _usuarioBusiness.FindById(IdUsuario);
            if (adm.PerfilUsuario != PerfilUsuario.Administrador)
                throw new ArgumentException("Usuário não permitido a realizar operação!");

            return Ok(_usuarioBusiness.FindAll(IdUsuario));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return Ok(new List<UsuarioDto>());
        }
    }
            
    [HttpGet("GetUsuario")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(UsuarioDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetUsuario()
    {
        try
        { 
            UsuarioDto _usuario = _usuarioBusiness.FindById(IdUsuario);
            if (_usuario == null) throw new Exception();
            return Ok(_usuario);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Usuário não encontrado!");
        }
    }

    [HttpPost]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(UsuarioDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Post([FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            usuarioDto.IdUsuario = IdUsuario;
            return new OkObjectResult(_usuarioBusiness.Create(usuarioDto));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao cadastar novo Usuário!");
        }
    }

    [HttpPut]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(UsuarioDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Put([FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            UsuarioDto updateUsuario = _usuarioBusiness.Update(usuarioDto);
            if (updateUsuario == null) 
                throw new ArgumentException("Usuário não encontrado!");

            return new OkObjectResult(updateUsuario);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao atualizar Usuário!");
        }
    }

    [HttpPut("UpdateUsuario")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(UsuarioDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult PutAdministrador([FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            var usuario = _usuarioBusiness.FindById(IdUsuario);
            if (usuario.PerfilUsuario != PerfilUsuario.Administrador)
                throw new ArgumentException("Usuário não permitido a realizar operação!");

            UsuarioDto updateUsuario = _usuarioBusiness.Update(usuarioDto);
            if (updateUsuario == null) 
                throw new ArgumentException("Usuário não encontrado!");

            return new OkObjectResult(updateUsuario);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao atualizar Usuário!");
        }
    }

    [HttpDelete]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Delete([FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            var adm = _usuarioBusiness.FindById(IdUsuario);
            if (adm.PerfilUsuario != PerfilUsuario.Administrador)
                throw new ArgumentException("Usuário não permitido a realizar operação!");

            if (_usuarioBusiness.Delete(usuarioDto))
                return new OkObjectResult(true);
            else
                throw new Exception();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao excluir Usuário!");
        }
    }
    
    [HttpGet("ImagemPerfil")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(ImagemPerfilDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetImage()
    {
        try
        {

            var imagemPerfilUsuario = _imagemPerfilBussiness
                .FindAll(IdUsuario).Find(prop => prop.IdUsuario.Equals(IdUsuario));

            if (imagemPerfilUsuario != null)
                return Ok(imagemPerfilUsuario);
            else
                throw new Exception();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Usuário não possui nenhuma imagem de perfil cadastrada!");
        }
    }

    [HttpPost("ImagemPerfil")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(ImagemPerfilDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PostImagemPerfil(IFormFile file)
    {
        try
        {
            var imagemPerfilUsuario = await ConvertFileToImagemPerfilUsuarioDtoAsync(file, IdUsuario);
            ImagemPerfilDto? _imagemPerfilUsuario = _imagemPerfilBussiness.Create(imagemPerfilUsuario);

            if (_imagemPerfilUsuario != null)
                return Ok(_imagemPerfilUsuario);
            else
                throw new Exception();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao incluir imagem de perfil!");
        }
    }

    [HttpPut("ImagemPerfil")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(ImagemPerfilDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PutImagemPerfil(IFormFile file)
    {
        try
        {
            var imagemPerfilUsuario = await ConvertFileToImagemPerfilUsuarioDtoAsync(file, IdUsuario);
            imagemPerfilUsuario = _imagemPerfilBussiness.Update(imagemPerfilUsuario);
            if (imagemPerfilUsuario != null)
                return Ok(imagemPerfilUsuario);
            else
                throw new Exception();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao atualizar imagem de perfil!");
        }
    }

    [HttpDelete("ImagemPerfil")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult DeleteImagemPerfil()
    {
        try
        {
            if (_imagemPerfilBussiness.Delete(IdUsuario))
                return Ok(true);
            else
                throw new Exception();
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

                ImagemPerfilDto imagemPerfilUsuario = new ImagemPerfilDto {

                    Name = fileName,
                    Type = typeFile,
                    ContentType = file.ContentType,
                    IdUsuario = IdUsuario,
                    Arquivo = memoryStream.GetBuffer()
                };
                return imagemPerfilUsuario;
            }
        }
        else
            throw new ArgumentException("Apenas arquivos do tipo jpg, jpeg ou png são aceitos.");
    }
    private bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
}