using Domain.Entities.Abstractions;

namespace Business.Abstractions;
public abstract class BusinessBase<Dto, Entity> where Dto : class where Entity : class, new()
{
    protected IUnitOfWork<Entity> _unitOfWork;
    protected BusinessBase(IUnitOfWork<Entity> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public abstract Dto Create(Dto usuarioVM);

    public abstract Dto FindById(int id, int idUsuario);

    public abstract IList<Dto> FindAll(int idUsuario);

    public abstract  Dto Update(Dto usuario);

    public abstract  bool Delete(Dto usuario);
}