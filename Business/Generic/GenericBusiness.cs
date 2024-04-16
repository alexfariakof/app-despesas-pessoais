using Domain.Core;
using Repository.Persistency.Generic;

namespace Business.Generic;
public class GenericBusiness<T> : IBusiness<T> where T : BaseModel
{
    private readonly IRepositorio<T> _repositorio;

    public GenericBusiness(IRepositorio<T> repositorio)
    {
        _repositorio = repositorio;
    }
    public T Create(T obj)
    {
        _repositorio.Insert(ref obj);
        return obj;
    }

    public List<T> FindAll(int idUsuario)
    {
        return _repositorio.GetAll();
    }

    public T FindById(int id, int idUsuario)
    {
        return _repositorio.Get(id);
    }

    public T Update(T obj)
    {
        _repositorio.Update(ref obj);
        return obj;
    }

    public bool Delete(T obj)
    {
        return _repositorio.Delete(obj);
    }

    public List<T> FindByIdUsuario(int idUsuario)
    {
        return new List<T>();
    }
}
