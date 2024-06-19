using AutoMapper;
using Repository.Persistency.Generic;
using Repository.Persistency.UnitOfWork.Abstractions;

namespace Business.Abstractions;
public abstract class BusinessBase<Dto, Entity>: IBusinessBase<Dto, Entity> where Dto : class where Entity : class, new()
{
    protected IUnitOfWork<Entity>? UnitOfWork { get;  }
    protected IMapper Mapper { get; set; }

    protected IRepositorio<Entity> Repository { get; }

    protected BusinessBase(IMapper mapper, IRepositorio<Entity> repository, IUnitOfWork<Entity>? unitOfWork = null)
    {
        Repository = repository;
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }

    public abstract Dto Create(Dto dto);

    public virtual Dto FindById(int id)
    {
        throw new NotImplementedException("Este método não foi implementado.");
    }

    public virtual Dto FindById(int id, int idUsuario) { return null; }

    public abstract List<Dto> FindAll(int idUsuario);

    public abstract  Dto Update(Dto dto);

    public abstract  bool Delete(Dto dto);
}