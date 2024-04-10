using Domain.VM;

namespace Business;
public interface IImagemPerfilUsuarioBusiness
{
    ImagemPerfilVM Create(ImagemPerfilVM obj);
    ImagemPerfilVM FindById(int id, int idUsuario);
    List<ImagemPerfilVM> FindAll(int idUsuario);
    ImagemPerfilVM Update(ImagemPerfilVM obj);
    bool Delete(int idUsuario);
    UsuarioVM FindByIdUsuario(int idUsuario);
}
