using Business.Dtos;

namespace Business.Abstractions;
public interface IUsuarioBusiness
{
    UsuarioDto Create(UsuarioDto usuarioVM);
    UsuarioDto FindById(int id);
    List<UsuarioDto> FindAll(int idUsuario);
    UsuarioDto Update(UsuarioDto usuarioVM);
    bool Delete(UsuarioDto usuarioVM);
}
