using Business.Dtos.Core;

namespace Business.Abstractions;
public interface IImagemPerfilUsuarioBusiness
{
    BaseImagemPerfilDto Create(BaseImagemPerfilDto obj);
    BaseImagemPerfilDto FindById(int id, int idUsuario);
    List<BaseImagemPerfilDto> FindAll(int idUsuario);
    BaseImagemPerfilDto Update(BaseImagemPerfilDto obj);
    bool Delete(int idUsuario);
    BaseUsuarioDto FindByIdUsuario(int idUsuario);
}
