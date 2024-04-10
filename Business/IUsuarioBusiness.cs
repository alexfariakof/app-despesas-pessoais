using Domain.VM;

namespace Business;
public interface IUsuarioBusiness
{
    UsuarioVM Create(UsuarioVM obj);
    UsuarioVM FindById(int id);
    List<UsuarioVM> FindAll(int idUsuario);
    UsuarioVM Update(UsuarioVM obj);
    bool Delete(UsuarioVM usuarioVM);
}
