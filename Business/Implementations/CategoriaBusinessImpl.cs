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
        public CategoriaBusinessImpl(IRepositorio<Categoria> repositorio)
        {
            _repositorio = repositorio;
        }
        public CategoriaVM Create(CategoriaVM obj)
        {
            Categoria categoria = _converter.Parse(obj);
            return _converter.Parse(_repositorio.Insert(categoria)); ;
        }

        public List<CategoriaVM> FindAll()
        {
            return _converter.ParseList(_repositorio.GetAll());
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

        public void Delete(int id)
        {
            _repositorio.Delete(id);
        }
    }
}
