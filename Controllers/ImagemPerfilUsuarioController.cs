using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemPerfilUsuarioController : ControllerBase
    {
        private IBusiness<ImagemPerfilUsuarioVM> _perfilFileBusiness;
        private string bearerToken;
        public ImagemPerfilUsuarioController(IBusiness<ImagemPerfilUsuarioVM> perfilFileBusiness)
        {
            _perfilFileBusiness = perfilFileBusiness;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            return Ok(_perfilFileBusiness.FindAll(_idUsuario.Value));
        }

        [HttpGet("GetByIdUsuario/{idUsuario}")]
        [Authorize("Bearer")]
        public IActionResult GetByIdUsuario([FromRoute] int idUsuario)
        {
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            if (_idUsuario.Value != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            var imagemPerfilUsuario = _perfilFileBusiness.FindAll(_idUsuario.Value)
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
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            if (_idUsuario.Value != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

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

                        ImagemPerfilUsuarioVM? _imagemPerfilUsuario = _perfilFileBusiness.Create(imagemPerfilUsuario);
                        if (_imagemPerfilUsuario != null)
                            return Ok(new { message = true, imagemPerfilUsuario = _imagemPerfilUsuario });
                        else
                            return Ok(new { message = false, imagemPerfilUsuario = _imagemPerfilUsuario });
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
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            if (_idUsuario.Value != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

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
                            return Ok(new { message = true, imagemPerfilUsuario = imagemPerfilUsuario });
                        else
                            return Ok(new { messsage = false, imagemPerfilUsuario = imagemPerfilUsuario });
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
            bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            var _idUsuario = ControleAcessoBusinessImpl.getIdUsuarioFromToken(bearerToken);

            if (_idUsuario.Value != idUsuario)
            {
                return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
            }

            try
            {
                if (_perfilFileBusiness.Delete(idUsuario))
                    return Ok(new { message = true });
                else
                    return Ok(new { message = false });
            }
            catch
            {
                return BadRequest(new { message = "Erro ao excluir imagem do perfil!" });
            }

        }
    }
}