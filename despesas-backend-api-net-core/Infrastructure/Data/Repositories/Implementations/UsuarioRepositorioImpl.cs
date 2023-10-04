using despesas_backend_api_net_core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using despesas_backend_api_net_core.Infrastructure.Security.Configuration;

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
                DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();
                DbSet<Categoria> dsCategoria = _context.Set<Categoria>();

                var controleAcesso = new ControleAcesso
                {
                    Login = item.Email,
                    UsuarioId = item.Id,
                    Usuario = item,
                    Senha = Crypto.GetInstance.Encrypt(Guid.NewGuid().ToString().Substring(0, 8))
                };
                dsControleACesso.Add(controleAcesso);

                List<Categoria> lstCategoria = new List<Categoria>();
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Alimentação",
                    TipoCategoria = TipoCategoria.Despesa
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Casa",
                    TipoCategoria = TipoCategoria.Despesa
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Serviços",
                    TipoCategoria = TipoCategoria.Despesa
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Saúde",
                    TipoCategoria = TipoCategoria.Despesa
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Imposto",
                    TipoCategoria = TipoCategoria.Despesa
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Transporte",
                    TipoCategoria = TipoCategoria.Despesa
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Lazer",
                    TipoCategoria = TipoCategoria.Despesa
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Outros",
                    TipoCategoria = TipoCategoria.Despesa
                });

                lstCategoria.Add(new Categoria
                {
                    Descricao = "Salário",
                    TipoCategoria = TipoCategoria.Receita
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Prêmio",
                    TipoCategoria = TipoCategoria.Receita
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Investimento",
                    TipoCategoria = TipoCategoria.Receita
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Benefício",
                    TipoCategoria = TipoCategoria.Receita
                });
                lstCategoria.Add(new Categoria
                {
                    Descricao = "Outros",
                    TipoCategoria = TipoCategoria.Receita
                });
                foreach (Categoria categoria in lstCategoria)
                {
                    categoria.Usuario = controleAcesso.Usuario;
                    dsCategoria.Add(categoria);
                }

                _context.SaveChanges();
            }
            catch 
            {
                throw new Exception("Erro ao inserir um novo usuário!");
            }
            return item;
        }
        List<Usuario> IRepositorio<Usuario>.GetAll()
        {
            try
            {
                return dataSet.ToList();
            }
            catch 
            {
                throw new Exception("Erro ao gerar resgistros de todos os usuários!");
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
            var controleAcesso = dsControleACesso.SingleOrDefault(prop => prop.UsuarioId.Equals(obj.Id));

            if (controleAcesso is null)
                return null;

            try
            {
                controleAcesso.Login = obj.Email;
                _context.Entry(controleAcesso).CurrentValues.SetValues(controleAcesso);
                var usaurio = dataSet.SingleOrDefault(prop => prop.Id.Equals(obj.Id));
                _context.Entry(usaurio).CurrentValues.SetValues(obj);
                _context.SaveChanges();
            }
            catch 
            {
                throw new Exception("Erro ao atualizar usuário!");
            }
            return obj;
        }

        bool IRepositorio<Usuario>.Delete(int id)
        {
            var result = dataSet.SingleOrDefault(prop => prop.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    result.StatusUsuario = StatusUsuario.Inativo;
                    _context.Entry(result).CurrentValues.SetValues(result);
                    _context.SaveChanges();
                    return true;
                }
                return false;                
            }
            catch 
            {
                throw new Exception("Erro ao deletar usuário!");
            }
        }

        public bool Exists(int? id)
        {
            return dataSet.Any(prop => prop.Id.Equals(id));
        }
    }
}