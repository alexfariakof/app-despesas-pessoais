using CrossCutting.CQRS.Queries;
using Domain.Entities.Abstractions;

namespace Business.Abstractions;
public abstract class BusinessBase<Dto, Entity> where Dto : class where Entity : class, new()
{
    protected IUnitOfWork<Entity> UnitOfWork { get;  }
    protected BusinessBase(IUnitOfWork<Entity> unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public abstract Dto Create(Dto usuarioDto);

    public abstract Dto FindById(int id, int idUsuario);

    public abstract Task<IList<Dto>> FindAll(int idUsuario);

    public abstract  Dto Update(Dto usuario);

    public abstract  bool Delete(Dto usuario);
}