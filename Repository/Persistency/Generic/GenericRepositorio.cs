using Domain.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Persistency.Generic;
public class GenericRepositorio<T> : IRepositorio<T> where T : BaseModel
{
    protected readonly RegisterContext _context;

    private DbSet<T> dataSet;

    public GenericRepositorio(RegisterContext context)
    {
        _context = context;
        dataSet = context.Set<T>();
    }
    public void Insert(ref T item)
    {
        try
        {
            dataSet.Add(item);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("GenericRepositorio_Insert", ex); ;
        }
    }
    public List<T> GetAll()
    {
        try
        {
            return dataSet.ToList();
        }
        catch(Exception ex)
        {
            throw new Exception("GenericRepositorio_GetAll", ex);
        }
    }
    public T Get(int id)
    {
        return dataSet.SingleOrDefault(prop => prop.Id.Equals(id));
    }
    public void Update(ref T obj)
    {
        try
        {
            _context.Update(obj);
            _context.SaveChanges();
        }
        catch (Exception ex)            
        {
            throw new Exception("GenericRepositorio_Update", ex);
        }
    }
    public bool Delete(T obj)
    {
        try
        {
            T result = dataSet.SingleOrDefault(prop => prop.Id.Equals(obj.Id));
            if (result != null)
            {                    
                if (result.GetType().Equals(typeof(Usuario)))
                {
                    var dataSet = _context.Set<Usuario>();
                    Usuario usaurio = new Usuario
                    {
                        Id = obj.Id,
                        StatusUsuario = StatusUsuario.Inativo
                    };
                    _context.Entry(result).CurrentValues.SetValues(usaurio);
                }
                else
                {
                    _context.Remove(result);
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception("GenericRepositorio_Delete", ex);
        }
    }
    public bool Exists(int? id)
    {
        return dataSet.Any(prop => prop.Id.Equals(id));
    }
}