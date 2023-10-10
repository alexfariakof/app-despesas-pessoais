using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class ReceitaBusinessImpl : IBusiness<ReceitaVM>
    {
        private readonly IRepositorio<Receita> _repositorio;
        private readonly ReceitaMap _converter;

        public ReceitaBusinessImpl(IRepositorio<Receita> repositorio)
        {
            _repositorio = repositorio;
            _converter = new ReceitaMap();
        }
        public ReceitaVM Create(ReceitaVM obj)
        {
            Receita receita = _repositorio.Insert(_converter.Parse(obj));
            return _converter.Parse(receita);
        }

        public List<ReceitaVM> FindAll(int idUsuario)
        {
            return _converter.ParseList(_repositorio.GetAll().FindAll(r => r.UsuarioId == idUsuario));
        }      

        public ReceitaVM FindById(int id, int idUsuario)
        {
            var receita = _converter.Parse(_repositorio.Get(id));
            if (receita.IdUsuario == idUsuario)
                return receita;
            return null;
        }

        public ReceitaVM Update(ReceitaVM obj)
        {

            Receita receita = _repositorio.Update(_converter.Parse(obj));
            return _converter.Parse(receita);

        }

        public bool Delete(int id)
        {
            return  _repositorio.Delete(new Receita { Id = id } );
        }
    }
}
