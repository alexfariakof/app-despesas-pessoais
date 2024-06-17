using Business.Dtos.Core;

namespace Business.Abstractions;
public interface IUsuarioBusiness<Dto> where Dto : UsuarioDtoBase, new()
{
    Dto Create(Dto dto);
    Dto FindById(int id);
    List<Dto> FindAll(int idUsuario);
    Dto Update(Dto dto);
    bool Delete(Dto dto);
}
