using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace apiDespesasPessoais.Business.Implementations
{
    public class DespesaBusinessImpl : IBusiness<Despesa>
    {
        private readonly IRepositorio<Despesa> _repositorio;

        public DespesaBusinessImpl(IRepositorio<Despesa> repositorio)
        {
            _repositorio = repositorio;
        }
        public Despesa Create(Despesa obj)
        {
            return _repositorio.Insert(obj);
        }

        public List<Despesa> FindAll()
        {
            return _repositorio.GetAll();
        }      

        public Despesa FindById(int id)
        {
            return _repositorio.Get(id);
        }

        public Despesa Update(Despesa obj)
        {           

            return _repositorio.Update(obj);
        }

        public void Delete(int id)
        {
            _repositorio.Delete(id);
        }
    }
}
