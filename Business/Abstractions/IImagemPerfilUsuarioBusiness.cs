using Business.Dtos;

namespace Business.Abstractions;
public interface IImagemPerfilUsuarioBusiness
{
    ImagemPerfilDto Create(ImagemPerfilDto obj);
    ImagemPerfilDto FindById(int id, int idUsuario);
    List<ImagemPerfilDto> FindAll(int idUsuario);
    ImagemPerfilDto Update(ImagemPerfilDto obj);
    bool Delete(int idUsuario);
    UsuarioDto FindByIdUsuario(int idUsuario);
}
