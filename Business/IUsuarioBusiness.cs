using Business.Dtos;

namespace Business;
public interface IUsuarioBusiness
{
    UsuarioVM Create(UsuarioVM usuarioVM);
    UsuarioVM FindById(int id);
    List<UsuarioVM> FindAll(int idUsuario);
    UsuarioVM Update(UsuarioVM usuarioVM);
    bool Delete(UsuarioVM usuarioVM);
}
