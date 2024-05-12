using AutoMapper;
using Repository.Persistency.UnitOfWork.Abstractions;

namespace Business.Abstractions;
public abstract class BusinessBase<Dto, Entity> where Dto : class where Entity : class, new()
{
    protected IUnitOfWork<Entity> UnitOfWork { get;  }
    protected IMapper Mapper { get; set; }
    protected BusinessBase(IMapper mapper, IUnitOfWork<Entity> unitOfWork)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }

    public abstract Dto Create(Dto usuarioDto);

    public abstract Dto FindById(int id, int idUsuario);

    public abstract List<Dto> FindAll(int idUsuario);

    public abstract  Dto Update(Dto usuario);

    public abstract  bool Delete(Dto usuario);
}