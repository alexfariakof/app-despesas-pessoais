using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class UsuarioBusinessImpl : IBusiness<Usuario>
    {
        private IRepositorio<Usuario> _repositorio;

        public UsuarioBusinessImpl(IRepositorio<Usuario> repositorio)
        {
            _repositorio = repositorio;

        }
        public Usuario Create(Usuario usuario)
        {
            return _repositorio.Insert(usuario);
        }

        public List<Usuario> FindAll()
        {
            return _repositorio.GetAll();
        }      

        public Usuario FindById(int idUsuario)
        {
            return _repositorio.Get(idUsuario);
        }

        public Usuario Update(Usuario usuario)
        {           

            return _repositorio.Update(usuario);
        }

        public void Delete(int idUsuario)
        {
            _repositorio.Delete(idUsuario);
        }
    }
}
