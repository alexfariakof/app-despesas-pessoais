using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic
{
    public class GenericRepositorio<T> : IRepositorio<T> where T : BaseModel
    {
        protected readonly RegisterContext _context;

        private DbSet<T> dataSet;

        public GenericRepositorio(RegisterContext context)
        {
            _context = context;
            dataSet = context.Set<T>();
        }

        public T Insert(T item)
        {
            try
            {
                dataSet.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

        public List<T> GetAll()
        {
            try
            {
                return dataSet.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public T Get(int id)
        {
            return dataSet.SingleOrDefault(prop => prop.Id.Equals(id));
        }

        public T Update(T obj)
        {
            if (!Exists(obj.Id))
                return null;

            T result = dataSet.SingleOrDefault(prop => prop.Id.Equals(obj.Id));
            try
            {
                _context.Entry(result).CurrentValues.SetValues(obj);
                _context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(BaseModel obj)
        {
            T result = dataSet.SingleOrDefault(prop => prop.Id.Equals(obj.Id));
            try 
            {
                if (result != null)
                { 
                    if (result.Equals(typeof(Usuario)))
                    {
                        var dataSet = _context.Set<Usuario>();
                        Usuario usaurio = new Usuario { 
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
            catch 
            {
                return false;
            }            
        }
        public bool Exists(int? id)
        {
            return dataSet.Any(prop => prop.Id.Equals(id));
        }

    }
}
