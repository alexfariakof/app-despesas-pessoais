using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class CategoriaBusinessImpl : IBusiness<CategoriaVM>
    {
        private readonly IRepositorio<Categoria> _repositorio;
        private readonly CategoriaMap _converter;
        private IRepositorio<CategoriaVM> @object;

        public CategoriaBusinessImpl(IRepositorio<Categoria> repositorio)
        {
            _repositorio = repositorio;
            _converter = new CategoriaMap();
        }

        public CategoriaVM Create(CategoriaVM obj)
        {
            Categoria categoria = _converter.Parse(obj);            
            return _converter.Parse(_repositorio.Insert(categoria)); ;
        }

        public List<CategoriaVM> FindAll()
        {
            var lstCategoria = _repositorio.GetAll();
            return _converter.ParseList(lstCategoria);
        }      

        public CategoriaVM FindById(int id)
        {
            return _converter.Parse(_repositorio.Get(id));
        }

        public CategoriaVM Update(CategoriaVM obj)
        {
            Categoria categoria = _converter.Parse(obj);
            return _converter.Parse(_repositorio.Update(categoria));
        }

        public bool Delete(int id)
        {
            return _repositorio.Delete(id);
        }

        public List<CategoriaVM> FindByIdUsuario(int idUsuario)
        {
            var lstCategoria = _repositorio.GetAll().FindAll(p => p.UsuarioId.Equals(idUsuario));
            return _converter.ParseList(lstCategoria);
        }
    }
}