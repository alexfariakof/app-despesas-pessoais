using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.v2;
using Business.HyperMedia.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers.v2;

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
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(UsuarioDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetUsuarioById()
    {
        try
        {
            var _usuario = _usuarioBusiness.FindById(IdUsuario);
            if (_usuario == null) throw new Exception();
            return Ok(_usuario);
        }
        catch(Exception ex) 
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Usuário não encontrado!");
        }
    }

    [HttpPut]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(UsuarioDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put([FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            usuarioDto.UsuarioId = IdUsuario;            
            return new OkObjectResult(_usuarioBusiness.Update(usuarioDto));
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                return BadRequest(argEx.Message);

            return BadRequest("Erro ao atualizar Usuário!");
        }
    }

    [HttpGet("ImagemPerfil")]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(ImagemPerfilDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetImage()
    {
        try
        {

            var imagemPerfilUsuario = _imagemPerfilBussiness.FindAll(IdUsuario).Find(prop => prop.UsuarioId.Equals(IdUsuario));

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
    [ProducesResponseType(200, Type = typeof(ImagemPerfilDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
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
    [ProducesResponseType(200, Type = typeof(ImagemPerfilDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
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
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
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