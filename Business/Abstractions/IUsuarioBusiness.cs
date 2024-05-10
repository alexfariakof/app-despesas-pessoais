using Business.Dtos.Core;

namespace Business.Abstractions;
public interface IUsuarioBusiness<Dto> where Dto : BaseUsuarioDto, new()
{
    Dto Create(Dto usuarioDto);
    Dto FindById(int id);
    List<Dto> FindAll(int idUsuario);
    Dto Update(Dto usuarioDto);
    bool Delete(Dto usuarioDto);
}
