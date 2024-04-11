using Business;
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
    public IActionResult Get()
    {
        var adm = _usuarioBusiness.FindById(IdUsuario);
        if (adm.PerfilUsuario != PerfilUsuario.Administrador)
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }

        return Ok(_usuarioBusiness.FindAll(IdUsuario));
    }
            
    [HttpGet("GetUsuario")]
    [Authorize("Bearer")]
    public IActionResult GetUsuario()
    {
        UsuarioVM _usuario = _usuarioBusiness.FindById(IdUsuario);
        if (_usuario == null)
            return BadRequest(new { message ="Usuário não encontrado!" });

        return Ok(_usuario);
    }

    [HttpPost]
    [Authorize("Bearer")]
    public IActionResult Post([FromBody] UsuarioVM usuarioVM)
    {
        var usuario = _usuarioBusiness.FindById(IdUsuario);
        if (usuario.PerfilUsuario != PerfilUsuario.Administrador)
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }

        if (String.IsNullOrEmpty(usuarioVM.Telefone) || String.IsNullOrWhiteSpace(usuarioVM.Telefone))
            return BadRequest(new { message = "Campo Telefone não pode ser em branco" });

        if (String.IsNullOrEmpty(usuarioVM.Email) || String.IsNullOrWhiteSpace(usuarioVM.Email))
            return BadRequest(new { message = "Campo Login não pode ser em branco" });

        if (!IsValidEmail(usuarioVM.Email))
            return BadRequest(new { message = "Email inválido!" });

        return new OkObjectResult(_usuarioBusiness.Create(usuarioVM));
    }

    [HttpPut]
    [Authorize("Bearer")]
    public IActionResult Put([FromBody] UsuarioVM usuarioVM)
    {
        if (String.IsNullOrEmpty(usuarioVM.Telefone) || String.IsNullOrWhiteSpace(usuarioVM.Telefone))
            return BadRequest(new { message = "Campo Telefone não pode ser em branco" });

        if (String.IsNullOrEmpty(usuarioVM.Email) || String.IsNullOrWhiteSpace(usuarioVM.Email))
            return BadRequest(new { message = "Campo Login não pode ser em branco" });

        if (!IsValidEmail(usuarioVM.Email))
            return BadRequest(new { message = "Email inválido!" });

        UsuarioVM updateUsuario = _usuarioBusiness.Update(usuarioVM);
        if (updateUsuario == null)
            return  BadRequest(new { message = "Usuário não encontrado!" });

        return new OkObjectResult(updateUsuario);
    }

    [HttpPut("UpdateUsuario")]
    [Authorize("Bearer")]
    public IActionResult PutAdministrador([FromBody] UsuarioVM usuarioVM)
    {
        var usuario = _usuarioBusiness.FindById(IdUsuario);
        if (usuario.PerfilUsuario != PerfilUsuario.Administrador)
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }

        if (String.IsNullOrEmpty(usuarioVM.Telefone) || String.IsNullOrWhiteSpace(usuarioVM.Telefone))
            return BadRequest(new { message = "Campo Telefone não pode ser em branco" });

        if (String.IsNullOrEmpty(usuarioVM.Email) || String.IsNullOrWhiteSpace(usuarioVM.Email))
            return BadRequest(new { message = "Campo Login não pode ser em branco" });

        if (!IsValidEmail(usuarioVM.Email))
            return BadRequest(new { message = "Email inválido!" });

        UsuarioVM updateUsuario = _usuarioBusiness.Update(usuarioVM);
        if (updateUsuario == null)
            return BadRequest(new { message = "Usuário não encontrado!" });

        return new OkObjectResult(updateUsuario);
    }

    [HttpDelete]
    [Authorize("Bearer")]
    public IActionResult Delete([FromBody] UsuarioVM usuarioVM)
    {
        var adm = _usuarioBusiness.FindById(IdUsuario);
        if (adm.PerfilUsuario != PerfilUsuario.Administrador)
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }
            
        if (_usuarioBusiness.Delete(usuarioVM))
            return new OkObjectResult(new { message = true });
        else
            return BadRequest(new { message = "Erro ao excluir Usuário!" });
    }
    
    [HttpGet("ImagemPerfil")]
    [Authorize("Bearer")]
    public IActionResult GetImage()
    {
        var imagemPerfilUsuario = _imagemPerfilBussiness.FindAll(IdUsuario)
            .Find(prop => prop.IdUsuario.Equals(IdUsuario));

        if (imagemPerfilUsuario != null)
            return Ok(new { message = true, imagemPerfilUsuario = imagemPerfilUsuario });
        else
            return BadRequest(new { message = "Usuário não possui nenhuma imagem de perfil cadastrada!" });
    }

    [HttpPost("ImagemPerfil")]
    [Authorize("Bearer")]
    public async Task<IActionResult> PostImagemPerfil(IFormFile file)
    {
        try
        {
            var imagemPerfilUsuario = await ConvertFileToImagemPerfilUsuarioVMAsync(file, IdUsuario);
            ImagemPerfilVM? _imagemPerfilUsuario = _imagemPerfilBussiness.Create(imagemPerfilUsuario);
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
    [Authorize("Bearer")]
    public async Task<IActionResult> PutImagemPerfil(IFormFile file)
    {
        try
        {
            var imagemPerfilUsuario = await ConvertFileToImagemPerfilUsuarioVMAsync(file, IdUsuario);
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
    [Authorize("Bearer")]
    public IActionResult DeleteImagemPerfil()
    {
        try
        {
            if (_imagemPerfilBussiness.Delete(IdUsuario))
                return Ok(new { message = true });
            else
                return BadRequest(new { message = false });
        }
        catch
        {
            return BadRequest(new { message = "Erro ao excluir imagem do perfil!" });
        }
    }
    private async Task<ImagemPerfilVM> ConvertFileToImagemPerfilUsuarioVMAsync(IFormFile file, int idUsuario)
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

                ImagemPerfilVM imagemPerfilUsuario = new ImagemPerfilVM {

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
            throw new Exception("Apenas arquivos do tipo jpg, jpeg ou png são aceitos.");
    }
    private bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }

}

