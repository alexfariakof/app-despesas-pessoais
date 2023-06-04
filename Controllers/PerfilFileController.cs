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
    public class PerfilFileController : ControllerBase
    {
        private IBusiness<PerfilUsuarioFileVM> _perfilFileBusiness;
        public PerfilFileController(IBusiness<PerfilUsuarioFileVM> perfilFileBusiness)
        {
            _perfilFileBusiness = perfilFileBusiness;
        }

        [HttpGet]
        //[Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(_perfilFileBusiness.FindAll());
        }

        [HttpGet("GetByIdUsuario/{idUsuario}")]
        //[Authorize("Bearer")]
        public IActionResult GetByIdUsuario([FromRoute] int idUsuario)
        {
            var perfilFile = _perfilFileBusiness.FindAll()
                .FindAll(prop => prop.UsuarioId.Equals(idUsuario));

            if (perfilFile != null)
                return Ok(perfilFile);
            else
                return BadRequest(new { message = "Arquivo Inexistente!" });
        }

        [HttpPost]
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
                
                if (typeFile != "jpg" || typeFile != "png")
                    return BadRequest(new { message = "Apenas arquivos do tipo jpg ou png são aceitos."});


                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    
                    PerfilUsuarioFileVM perfilUsuarioFile = new PerfilUsuarioFileVM
                    {
                        Arquivo = memoryStream.GetBuffer(),
                        UsuarioId = idUsuario,
                        Name = fileName,
                        Type = typeFile,
                        ContentType = file.ContentType
                    };

                    perfilUsuarioFile =  _perfilFileBusiness.Create(perfilUsuarioFile);
                    if (perfilUsuarioFile != null)
                        return Ok(perfilUsuarioFile);
                    else
                        return BadRequest(new { message = "Imagem de perfil não foi incluída!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message= "Erro ao incluir nova imagem de peefil!" });
            }
        }

        [HttpPut]
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

                if (typeFile != "jpg" || typeFile != "png")
                    return BadRequest(new { message = "Apenas arquivos do tipo jpg ou png são aceitos." });


                using (var memoryStream = new MemoryStream())
                {

                    await file.CopyToAsync(memoryStream);

                    PerfilUsuarioFileVM perfilUsuarioFile = new PerfilUsuarioFileVM
                    {
                        Arquivo = memoryStream.GetBuffer(),
                        UsuarioId = idUsuario,
                        Name = fileName,
                        Type = typeFile,
                        ContentType = file.ContentType
                    };

                    perfilUsuarioFile = _perfilFileBusiness.Update(perfilUsuarioFile);
                    if (perfilUsuarioFile != null)
                        return Ok(perfilUsuarioFile);
                    else
                        return BadRequest(new { messsage = "Imagem de perfil não foi atualizada!"});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao Atualizar iamgem do perfil!" });
            }
        }

        [HttpDelete("{idUsuario}")]
        //[Authorize("Bearer")]
        public IActionResult Delete(int idUsuario)
        {
            _perfilFileBusiness.Delete(idUsuario);
            return NoContent();
        }
    }
}
