using Domain.Core;

namespace Business.Abstractions.Generic;
public interface IBusiness<Dto, Entity> where Dto : class where Entity : BaseModel, new()
{
    Dto Create(Dto obj);
    Dto FindById(Guid id, Guid idUsuario);
    List<Dto> FindAll(Guid idUsuario);
    Dto Update(Dto obj);
    bool Delete(Dto obj);
}
