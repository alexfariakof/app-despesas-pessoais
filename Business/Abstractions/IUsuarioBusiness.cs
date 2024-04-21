using Business.Dtos;

namespace Business.Abstractions;
public interface IUsuarioBusiness
{
    UsuarioDto Create(UsuarioDto usuarioDto);
    UsuarioDto FindById(int id);
    List<UsuarioDto> FindAll(int idUsuario);
    UsuarioDto Update(UsuarioDto usuarioDto);
    bool Delete(UsuarioDto usuarioDto);
}
