using despesas_backend_api_net_core.Domain.VM;

namespace despesas_backend_api_net_core.Business
{
    public interface IUsuarioBusiness
    {
        UsuarioVM Create(UsuarioVM obj);
        UsuarioVM FindById(int id);
        List<UsuarioVM> FindAll(int idUsuario);
        UsuarioVM Update(UsuarioVM obj);
        bool Delete(int id);
    }
}
