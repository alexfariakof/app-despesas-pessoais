using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemPerfilUsuarioController : ControllerBase
    {
        private IBusiness<ImagemPerfilUsuarioVM> _perfilFileBusiness;
        public ImagemPerfilUsuarioController(IBusiness<ImagemPerfilUsuarioVM> perfilFileBusiness)
        {
            _perfilFileBusiness = perfilFileBusiness;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(_perfilFileBusiness.FindAll());
        }

        [HttpGet("GetByIdUsuario/{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult GetByIdUsuario([FromRoute] int idUsuario)
        {
            var imagemPerfilUsuario = _perfilFileBusiness.FindAll()
                .Find(prop => prop.IdUsuario.Equals(idUsuario));

            if (imagemPerfilUsuario != null)
                return Ok(new { message = true, imagemPerfilUsuario= imagemPerfilUsuario });
            else
                return Ok(new { message = false });
        }

        [HttpPost]
        [Authorize("Bearer")]
        public async Task<IActionResult> Post(int idUsuario, IFormFile file)
        {
            try
            {
                string fileName = "perfil-usuarioId-" + idUsuario + "-" + DateTime.Now.ToString("yyyyMMdd");
                string typeFile = "";
                int posicaoUltimoPontoNoArquivo = file.FileName.LastIndexOf('.');
                if (posicaoUltimoPontoNoArquivo >= 0 && posicaoUltimoPontoNoArquivo < file.FileName.Length - 1)
                {
                    typeFile = file.FileName.Substring(posicaoUltimoPontoNoArquivo + 1);
                }

                if (typeFile == "jpg" || typeFile == "png")
                {


                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);

                        ImagemPerfilUsuarioVM imagemPerfilUsuario = new ImagemPerfilUsuarioVM
                        {
                            Arquivo = memoryStream.GetBuffer(),
                            IdUsuario = idUsuario,
                            Name = fileName,
                            Type = typeFile,
                            ContentType = file.ContentType
                        };

                        var _imagemPerfilUsuario = _perfilFileBusiness.Create(imagemPerfilUsuario);
                        if (_imagemPerfilUsuario != null)
                            return Ok(new { message = true, imagemPerfilUsuario = _imagemPerfilUsuario });
                        else
                            return BadRequest(new { message = "Imagem de perfil não foi incluída!" });
                    }
                }
                else
                    return BadRequest(new { message = "Apenas arquivos do tipo jpg ou png são aceitos." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message= "Erro ao incluir nova imagem de peefil!" });
            }
        }

        [HttpPut]
        [Authorize("Bearer")]
        public async Task<IActionResult> Put(int idUsuario, IFormFile file)
        {
            try
            {
                string fileName = "perfil-usuarioId-" + idUsuario + "-" + DateTime.Now.ToString("yyyyMMdd");
                string typeFile = "";
                int posicaoUltimoPontoNoArquivo = file.FileName.LastIndexOf('.');
                if (posicaoUltimoPontoNoArquivo >= 0 && posicaoUltimoPontoNoArquivo < file.FileName.Length - 1)
                {
                    typeFile = file.FileName.Substring(posicaoUltimoPontoNoArquivo + 1);
                }

                if (typeFile == "jpg" || typeFile == "png")
                {


                    using (var memoryStream = new MemoryStream())
                    {

                        await file.CopyToAsync(memoryStream);

                        ImagemPerfilUsuarioVM imagemPerfilUsuario = new ImagemPerfilUsuarioVM
                        {
                            Arquivo = memoryStream.GetBuffer(),
                            IdUsuario = idUsuario,
                            Name = fileName,
                            Type = typeFile,
                            ContentType = file.ContentType
                        };

                        imagemPerfilUsuario = _perfilFileBusiness.Update(imagemPerfilUsuario);
                        if (imagemPerfilUsuario != null)
                            return Ok(new { messsage = true, imagemPerfilUsuario = imagemPerfilUsuario });
                        else
                            return BadRequest(new { messsage = "Imagem de perfil não foi atualizada!" });
                    }
                }
                else
                    return BadRequest(new { message = "Apenas arquivos do tipo jpg ou png são aceitos." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao Atualizar imagem do perfil!" });
            }
        }

        [HttpDelete("{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult Delete(int idUsuario)
        {

            if (_perfilFileBusiness.Delete(idUsuario))
                return Ok(new { message = "Imagem perfil excluida com sucesso!" });
            else
                return BadRequest(new { message = "Erro ao excluir imagem do perfil!" });

        }
    }
}
