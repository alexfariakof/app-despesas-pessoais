using AutoMapper;
using Domain.Core;
using Repository.Persistency.Generic;

namespace Business.Abstractions.Generic;
public class GenericBusiness<Dto, Entity> : IBusiness<Dto, Entity> where Dto : class where Entity : BaseModel, new()
{
    private readonly IRepositorio<Entity> _repositorio;
    private readonly IMapper _mapper;

    public GenericBusiness(IMapper mapper, IRepositorio<Entity> repositorio)
    {
        _mapper = mapper;
        _repositorio = repositorio;
    }
    public Dto Create(Dto obj)
    {
        var entity = _mapper.Map<Entity>(obj);
        _repositorio.Insert(ref entity);
        return _mapper.Map<Dto>(entity);
    }

    public List<Dto> FindAll(int idUsuario)
    {
        return _mapper.Map<List<Dto>>(_repositorio.GetAll());
    }

    public Dto FindById(int id, int idUsuario)
    {
        return _mapper.Map<Dto>(_repositorio.Get(id));
    }

    public Dto Update(Dto obj)
    {
        var entity = _mapper.Map<Entity>(obj);
        _repositorio.Update(ref entity);
        return _mapper.Map<Dto>(obj);
    }

    public bool Delete(Dto obj)
    {
        var entity = _mapper.Map<Entity>(obj);
        return _repositorio.Delete(entity);
    }
}
