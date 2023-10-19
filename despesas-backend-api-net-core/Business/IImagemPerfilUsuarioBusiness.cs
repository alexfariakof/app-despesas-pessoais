using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;

namespace despesas_backend_api_net_core.Business
{
    public interface IImagemPerfilUsuarioBusiness
    {
        ImagemPerfilUsuarioVM Create(ImagemPerfilUsuarioVM obj);
        ImagemPerfilUsuarioVM FindById(int id, int idUsuario);
        List<ImagemPerfilUsuarioVM> FindAll(int idUsuario);
        ImagemPerfilUsuarioVM Update(ImagemPerfilUsuarioVM obj);
        bool Delete(ImagemPerfilUsuarioVM obj);
        UsuarioVM FindByIdUsuario(int idUsuario);
    }
}
