

using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class PerfilFileBusinessImpl : IBusiness<PerfilUsuarioFileVM>
    {
        private readonly IRepositorio<PerfilFile> _repositorio;
        private readonly PerfilFileMap _converter;
        public PerfilFileBusinessImpl(IRepositorio<PerfilFile> repositorio)
        {
            _repositorio = repositorio;
            _converter = new PerfilFileMap();
        }

        public PerfilUsuarioFileVM Create(PerfilUsuarioFileVM obj)
        {
            string url = AmazonS3Bucket.WritingAnObjectAsync(obj).GetAwaiter().GetResult();
            obj.Url = url;
            PerfilFile perfilFile = _converter.Parse(obj);
            return _converter.Parse(_repositorio.Insert(perfilFile));
        }

        public List<PerfilUsuarioFileVM> FindAll()
        {
            var lstPerfilFile = _repositorio.GetAll();
            return _converter.ParseList(lstPerfilFile);
        }

        public PerfilUsuarioFileVM FindById(int id)
        {
            return _converter.Parse(_repositorio.Get(id));
        }

        public PerfilUsuarioFileVM Update(PerfilUsuarioFileVM obj)
        {
            PerfilFile perfilFile = _converter.Parse(obj);
            return _converter.Parse(_repositorio.Update(perfilFile));
        }

        public void Delete(int id)
        {
            _repositorio.Delete(id);
        }
    }
}