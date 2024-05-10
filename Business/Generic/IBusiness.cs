using Domain.Core;

namespace Business.Generic;
public interface IBusiness<Dto, Entity> where Dto : class where Entity : BaseModel, new ()
{
    Dto Create(Dto obj);
    Dto FindById(int id, int idUsuario);
    List<Dto> FindAll(int idUsuario);
    Dto Update(Dto obj);
    bool Delete(Dto obj);
}
