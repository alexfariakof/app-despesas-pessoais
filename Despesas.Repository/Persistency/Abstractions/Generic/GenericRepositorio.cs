using Domain.Core;
using Domain.Entities;
using System.Linq.Expressions;

namespace Repository.Persistency.Generic;
public class GenericRepositorio<T> : IRepositorio<T> where T : BaseModel, new()
{
    protected readonly RegisterContext _context;

    public GenericRepositorio(RegisterContext context)
    {
        _context = context;
    }

    public virtual void Insert(ref T item)
    {
        try
        {
            this._context.Add(item);
            this._context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("GenericRepositorio_Insert", ex); ;
        }
    }

    public virtual List<T> GetAll()
    {
        try
        {
            return this._context.Set<T>().ToList();
        }
        catch(Exception ex)
        {
            throw new Exception("GenericRepositorio_GetAll", ex);
        }
    }

    public virtual T Get(int id)
    {
        return this._context.Set<T>().SingleOrDefault(prop => prop.Id.Equals(id));
    }

    public virtual void Update(ref T entity)
    {
        try
        {
            var existingEntity = _context.Set<T>().Find(entity.Id);
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
        catch (Exception ex)            
        {
            throw new Exception("GenericRepositorio_Update", ex);
        }
    }

    public virtual bool Delete(T entity)
    {
        try
        {
            T result = this._context.Set<T>().SingleOrDefault(prop => prop.Id.Equals(entity.Id));
            if (result != null)
            {                    
                if (result.GetType().Equals(typeof(Usuario)))
                {
                    var dataSet = _context.Set<Usuario>();
                    Usuario usaurio = new Usuario
                    {
                        Id = entity.Id,
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

    public virtual bool Exists(int? id)
    {
        return this._context.Set<T>().Any(prop => prop.Id.Equals(id));
    }

    public IEnumerable<T>? Find(Expression<Func<T, bool>> expression)
    {
        return this._context.Set<T>().Where(expression);
    }
}