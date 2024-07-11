using Business.Dtos.Core;

namespace Business.Abstractions;
public interface IUsuarioBusiness<Dto> where Dto : UsuarioDtoBase, new()
{
    Dto Create(Dto dto);
    Dto FindById(Guid id);
    List<Dto> FindAll(Guid idUsuario);
    Dto Update(Dto dto);
    bool Delete(Dto dto);
}
