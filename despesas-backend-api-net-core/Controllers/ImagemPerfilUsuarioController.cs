using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ImagemPerfilUsuarioController : ControllerBase
    {
        private IImagemPerfilUsuarioBusiness _perfilFileBusiness;
        private string bearerToken;
        public ImagemPerfilUsuarioController(IImagemPerfilUsuarioBusiness perfilFileBusiness)
        {
            _perfilFileBusiness = perfilFileBusiness;
            bearerToken = String.Empty;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario =  bearerToken.getIdUsuarioFromToken();
            
            var imagemPerfilUsuario = _perfilFileBusiness.FindAll(_idUsuario)
                .Find(prop => prop.IdUsuario.Equals(_idUsuario));

            if (imagemPerfilUsuario != null)
                return Ok(new { message = true, imagemPerfilUsuario= imagemPerfilUsuario });
            else
                return BadRequest(new { message = "Usuário não possui nenhuma imagem de perfil cadastrada!" });
        }

        [HttpPost]
        [Authorize("Bearer")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var idUsuario = bearerToken.getIdUsuarioFromToken();

            try
            {
                string fileName = idUsuario + "-imagem-perfil-usuario-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string typeFile = "";
                int posicaoUltimoPontoNoArquivo = file.FileName.LastIndexOf('.');
                if (posicaoUltimoPontoNoArquivo >= 0 && posicaoUltimoPontoNoArquivo < file.FileName.Length - 1)
                {
                    typeFile = file.FileName.Substring(posicaoUltimoPontoNoArquivo + 1);
                }

                if (typeFile == "jpg" || typeFile == "png" || typeFile == "jpeg")
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

                        ImagemPerfilUsuarioVM? _imagemPerfilUsuario = _perfilFileBusiness.Create(imagemPerfilUsuario);
                        if (_imagemPerfilUsuario != null)
                            return Ok(new { message = true, imagemPerfilUsuario = _imagemPerfilUsuario });
                        else
                            return BadRequest(new { message = false, imagemPerfilUsuario = _imagemPerfilUsuario });
                    }
                }
                else
                    return BadRequest(new { message = "Apenas arquivos do tipo jpg, jpeg ou png são aceitos." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message= "Erro ao incluir nova imagem de peefil!" });
            }
        }

        [HttpPut]
        [Authorize("Bearer")]
        public async Task<IActionResult> Put(IFormFile file)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var idUsuario = bearerToken.getIdUsuarioFromToken();

            try
            {
                string fileName = idUsuario + "-imagem-perfil-usuario-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string typeFile = "";
                int posicaoUltimoPontoNoArquivo = file.FileName.LastIndexOf('.');
                if (posicaoUltimoPontoNoArquivo >= 0 && posicaoUltimoPontoNoArquivo < file.FileName.Length - 1)
                {
                    typeFile = file.FileName.Substring(posicaoUltimoPontoNoArquivo + 1);
                }

                if (typeFile == "jpg" || typeFile == "png" || typeFile == "jpeg")
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
                            return Ok(new { message = true, imagemPerfilUsuario = imagemPerfilUsuario });
                        else
                            return BadRequest(new { message = false, imagemPerfilUsuario = imagemPerfilUsuario });
                    }
                }
                else
                    return BadRequest(new { message = "Apenas arquivos do tipo jpg, jpeg ou png são aceitos." });
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
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = bearerToken.getIdUsuarioFromToken();

            if (_idUsuario != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            try
            {
                if (_perfilFileBusiness.Delete(new ImagemPerfilUsuarioVM { IdUsuario = idUsuario }))
                    return Ok(new { message = true });
                else
                    return BadRequest(new { message = false });
            }
            catch
            {
                return BadRequest(new { message = "Erro ao excluir imagem do perfil!" });
            }
        }
    }
}