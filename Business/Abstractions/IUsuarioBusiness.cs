using Business.Dtos.Core;

namespace Business.Abstractions;
public interface IUsuarioBusiness
{
    BaseUsuarioDto Create(BaseUsuarioDto usuarioDto);
    BaseUsuarioDto FindById(int id);
    List<BaseUsuarioDto> FindAll(int idUsuario);
    BaseUsuarioDto Update(BaseUsuarioDto usuarioDto);
    bool Delete(BaseUsuarioDto usuarioDto);
}
