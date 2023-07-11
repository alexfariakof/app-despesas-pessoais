

using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using despesas_backend_api_net_core.Infrastructure.Security.Configuration;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class ImagemPerfilUsuarioBusinessImpl : IBusiness<ImagemPerfilUsuarioVM>
    {
        private readonly IRepositorio<ImagemPerfilUsuario> _repositorio;
        private readonly ImagemPerfilUsuarioMap _converter;
        public ImagemPerfilUsuarioBusinessImpl(IRepositorio<ImagemPerfilUsuario> repositorio)
        {
            _repositorio = repositorio;
            _converter = new ImagemPerfilUsuarioMap();
        }
        public ImagemPerfilUsuarioVM Create(ImagemPerfilUsuarioVM obj)
        {
            try
            {
                string url = AmazonS3Bucket.WritingAnObjectAsync(obj).GetAwaiter().GetResult();
                obj.Url = url;
                ImagemPerfilUsuario perfilFile = _converter.Parse(obj);
                return _converter.Parse(_repositorio.Insert(perfilFile));
            }
            catch
            {
                AmazonS3Bucket.DeleteObjectNonVersionedBucketAsync(obj).GetAwaiter();
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
        public ImagemPerfilUsuarioVM Update(ImagemPerfilUsuarioVM obj)
        {
            var isPerfilValid = FindAll(obj.IdUsuario).Find(prop => prop.IdUsuario.Equals(obj.IdUsuario));
            if (isPerfilValid != null)
            {
                var result = AmazonS3Bucket.DeleteObjectNonVersionedBucketAsync(obj).GetAwaiter().GetResult();
                if (result)
                {
                    string url = AmazonS3Bucket.WritingAnObjectAsync(obj).GetAwaiter().GetResult();
                    isPerfilValid.Url = url;
                    ImagemPerfilUsuario perfilFile = _converter.Parse(isPerfilValid);
                    return _converter.Parse(_repositorio.Update(perfilFile));
                }
            }
            return null;            
        }
        public bool Delete(int idUsaurio)
        {
            var obj = FindAll(idUsaurio).Find(prop  => prop.IdUsuario.Equals(idUsaurio));
            if (obj != null)
            {
                var result = AmazonS3Bucket.DeleteObjectNonVersionedBucketAsync(obj).GetAwaiter().GetResult();
                if (result)
                {
                   return _repositorio.Delete(obj.Id);
                }
            }
            return false;
        }

        public List<ImagemPerfilUsuarioVM> FindByIdUsuario(int idUsuario)
        {
            var lstPerfilFile = _repositorio.GetAll().FindAll(p => p.UsuarioId.Equals(idUsuario));
            return _converter.ParseList(lstPerfilFile);
        }
    }
}