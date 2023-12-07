using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ImagemPerfilUsuarioController : AuthController
    {
        private IImagemPerfilUsuarioBusiness _perfilFileBusiness;
        public ImagemPerfilUsuarioController(IImagemPerfilUsuarioBusiness perfilFileBusiness)
        {
            _perfilFileBusiness = perfilFileBusiness;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        { 
            var imagemPerfilUsuario = _perfilFileBusiness.FindAll(IdUsuario)
                .Find(prop => prop.IdUsuario.Equals(IdUsuario));

            if (imagemPerfilUsuario != null)
                return Ok(new { message = true, imagemPerfilUsuario = imagemPerfilUsuario });
            else
                return BadRequest(new { message = "Usuário não possui nenhuma imagem de perfil cadastrada!" });
        }

        [HttpPost]
        [Authorize("Bearer")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            try
            {
                var imagemPerfilUsuario = await ConvertFileToImagemPerfilUsuarioVMAsync(file, IdUsuario);
                ImagemPerfilUsuarioVM? _imagemPerfilUsuario = _perfilFileBusiness.Create(imagemPerfilUsuario);
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

        [HttpPut]
        [Authorize("Bearer")]
        public async Task<IActionResult> Put(IFormFile file)
        {
            try
            {
                var imagemPerfilUsuario = await ConvertFileToImagemPerfilUsuarioVMAsync(file, IdUsuario);
                imagemPerfilUsuario = _perfilFileBusiness.Update(imagemPerfilUsuario);
                if (imagemPerfilUsuario != null)
                    return Ok(new { message = true, imagemPerfilUsuario = imagemPerfilUsuario });
                else
                    return BadRequest(new { message = false, imagemPerfilUsuario = imagemPerfilUsuario });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize("Bearer")]
        public IActionResult Delete()
        {
            try
            {
                if (_perfilFileBusiness.Delete(new ImagemPerfilUsuarioVM { IdUsuario = IdUsuario }))
                    return Ok(new { message = true });
                else
                    return BadRequest(new { message = false });
            }
            catch
            {
                return BadRequest(new { message = "Erro ao excluir imagem do perfil!" });
            }
        }   
        private async Task<ImagemPerfilUsuarioVM> ConvertFileToImagemPerfilUsuarioVMAsync(IFormFile file, int idUsuario)
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

                    ImagemPerfilUsuarioVM imagemPerfilUsuario = new ImagemPerfilUsuarioVM
                    {
                        Arquivo =  memoryStream.GetBuffer(),
                        IdUsuario = idUsuario,
                        Name = fileName,
                        Type = typeFile,
                        ContentType = file.ContentType
                    };
                    return imagemPerfilUsuario;
                }
            }
            else
                throw new Exception("Apenas arquivos do tipo jpg, jpeg ou png são aceitos.");
        }
    }
}