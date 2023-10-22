using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using despesas_backend_api_net_core.Infrastructure.Security;
using despesas_backend_api_net_core.Infrastructure.Security.Implementation;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class ImagemPerfilUsuarioBusinessImpl : IImagemPerfilUsuarioBusiness
    {
        private readonly IRepositorio<ImagemPerfilUsuario> _repositorio;
        private readonly ImagemPerfilUsuarioMap _converter;
        private readonly IAmazonS3Bucket _amazonS3Bucket;
            public ImagemPerfilUsuarioBusinessImpl(IRepositorio<ImagemPerfilUsuario> repositorio, IAmazonS3Bucket amazonS3Bucket = null)
        {
            _repositorio = repositorio;
            _converter = new ImagemPerfilUsuarioMap();
            _amazonS3Bucket = amazonS3Bucket == null ? AmazonS3Bucket.GetInstance : amazonS3Bucket; 
        }
        public ImagemPerfilUsuarioVM Create(ImagemPerfilUsuarioVM obj)
        {
            try
            {
                string url = _amazonS3Bucket.WritingAnObjectAsync(obj).GetAwaiter().GetResult();
                obj.Url = url;
                ImagemPerfilUsuario perfilFile = _converter.Parse(obj);
                return _converter.Parse(_repositorio.Insert(perfilFile));
            }
            catch
            {
                _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(obj).GetAwaiter();
            }
            return null; 
        }
        public List<ImagemPerfilUsuarioVM> FindAll(int idUsuario)
        {
            var lstPerfilFile = _repositorio.GetAll();
            return _converter.ParseList(lstPerfilFile);
        }
        public ImagemPerfilUsuarioVM FindById(int id, int idUsuario)
        {
            var imagemPerfilUsuario = _converter.Parse(_repositorio.Get(id));
            if (imagemPerfilUsuario.IdUsuario != idUsuario)
                return null;

            return imagemPerfilUsuario;
        }

        public UsuarioVM FindByIdUsuario(int idUsuario)
        {
            try
            {
                var usuario = _repositorio.GetAll().Find(u => u.UsuarioId == idUsuario).Usuario;
                return new UsuarioMap().Parse(usuario);
            }
            catch
            {
                return null;
            }            
        }

        public ImagemPerfilUsuarioVM Update(ImagemPerfilUsuarioVM obj)
        {
            var isPerfilValid = FindAll(obj.IdUsuario).Find(prop => prop.IdUsuario.Equals(obj.IdUsuario));
            if (isPerfilValid != null)
            {
                var result = _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(isPerfilValid).GetAwaiter().GetResult();
                if (result)
                {
                    string url = _amazonS3Bucket.WritingAnObjectAsync(obj).GetAwaiter().GetResult();
                    isPerfilValid.Url = url;
                    ImagemPerfilUsuario perfilFile = _converter.Parse(isPerfilValid);
                    return _converter.Parse(_repositorio.Update(perfilFile));
                }
            }
            return null;            
        }
        public bool Delete(ImagemPerfilUsuarioVM obj)
        {
            var imagemPerfilUsuario = FindAll(obj.IdUsuario).Find(prop  => prop.IdUsuario.Equals(obj.IdUsuario));
            if (imagemPerfilUsuario != null)
            {
                var result = _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(imagemPerfilUsuario).GetAwaiter().GetResult();
                if (result)
                {
                   return _repositorio.Delete(new ImagemPerfilUsuario { Id = imagemPerfilUsuario.Id });
                }
            }
            return false;
        }
    }
}