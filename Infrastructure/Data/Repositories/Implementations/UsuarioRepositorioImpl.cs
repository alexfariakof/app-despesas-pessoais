using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Infrastructure.Data.Repositories.Implementations
{
    public class UsuarioRepositorioImpl : IRepositorio<Usuario>
    {
        private readonly RegisterContext _context;
        private DbSet<Usuario> dataSet;
        public UsuarioRepositorioImpl(RegisterContext context)
        {
            _context = context;
            dataSet = context.Set<Usuario>();
        }

        Usuario IRepositorio<Usuario>.Insert(Usuario item)
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
        List<Usuario> IRepositorio<Usuario>.GetAll()
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
        Usuario IRepositorio<Usuario>.Get(int id)
        {
            return dataSet.SingleOrDefault(prop => prop.Id.Equals(id));
        }

        Usuario IRepositorio<Usuario>.Update(Usuario obj)
        {
            if (!Exists(obj.Id))
                return null;

            DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();
            var controleAcesso = dsControleACesso.SingleOrDefault(prop => prop.Id.Equals(obj.Id));
            try
            {
                controleAcesso.Login = obj.Email;
                _context.Entry(controleAcesso).CurrentValues.SetValues(controleAcesso);
                var usaurio = dataSet.SingleOrDefault(prop => prop.Id.Equals(obj.Id));
                _context.Entry(usaurio).CurrentValues.SetValues(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return obj;
        }


        void IRepositorio<Usuario>.Delete(int id)
        {
            var result = dataSet.SingleOrDefault(prop => prop.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    if (result.Equals(typeof(Usuario)))
                    {
                        var dataSet = _context.Set<Usuario>();
                        Usuario usaurio = new Usuario
                        {
                            Id = id,
                            StatusUsuario = StatusUsuario.Inativo
                        };
                        _context.Entry(result).CurrentValues.SetValues(usaurio);
                    }
                    else
                    {
                        _context.Remove(result);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(int? id)
        {
            return dataSet.Any(prop => prop.Id.Equals(id));
        }
    }
}