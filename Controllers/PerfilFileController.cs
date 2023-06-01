using despesas_backend_api_net_core.Business.Generic;
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

        [HttpPost("GetByIdUsuario")]
        //[Authorize("Bearer")]
        public IActionResult GetByIdUsuario([FromBody]int idUsuario)
        {
            var perfilFile = _perfilFileBusiness.FindAll()
                .FindAll(prop => prop.UsuarioId.Equals(idUsuario));
            
            if (perfilFile != null)  
                return Ok(perfilFile);

            return BadRequest("Arquivo Inexistente!");
        }


        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(int idUsuario, IFormFile file)
        {
            try
            {
                string fileName = "perfil-usuario-" + idUsuario + "-" + DateTime.Now.ToString("yyyyMMddMMHHmmssffffff");
                string typeFile = "";
                int posicaoUltimoPontoNoArquivo = file.FileName.LastIndexOf('.');
                if (posicaoUltimoPontoNoArquivo >= 0 && posicaoUltimoPontoNoArquivo < file.FileName.Length - 1)
                {
                    typeFile = file.FileName.Substring(posicaoUltimoPontoNoArquivo + 1);
                }

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
                    return Ok(perfilUsuarioFile);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex });
            }
        }


        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Upload(int idUsuario, int IdTipoLancamento, List<IFormFile> files)
        {

            try
            {
                long size = files.Sum(f => f.Length);

                // full path to file in temp location
                //var filePath = Path.GetTempFileName();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "FilesDownload");

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                // process uploaded files
                // Don't rely on or trust the FileName property without validation.

                return Ok(new { count = files.Count, size, filePath });
            }
            catch
            {
                return BadRequest();
            }            
        }
 
    }
}
