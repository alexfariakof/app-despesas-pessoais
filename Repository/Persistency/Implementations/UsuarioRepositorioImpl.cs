using Domain.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Persistency.Generic;

namespace Repository.Persistency.Implementations;
public class UsuarioRepositorioImpl : IRepositorio<Usuario>
{
    private readonly RegisterContext _context;
    private DbSet<Usuario> dataSet;
    public UsuarioRepositorioImpl(RegisterContext context)
    {
        _context = context;
        dataSet = context.Set<Usuario>();
    }
    public void Insert(ref Usuario item)
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
    }
    public List<Usuario> GetAll()
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
    public Usuario Get(int id)
    {
        return dataSet.Single(prop => prop.Id.Equals(id));
    }
    public void Update(ref Usuario obj)
    {
        DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();        
        try
        {
            var usuarioId = obj.Id;
            var controleAcesso = dsControleACesso.Single(prop => prop.UsuarioId.Equals(usuarioId));
            if (controleAcesso == null)
                throw new Exception();

            controleAcesso.Login = obj.Email;
            _context.Entry(controleAcesso).CurrentValues.SetValues(controleAcesso);
            var usaurio = dataSet.SingleOrDefault(prop => prop.Id.Equals(usuarioId));
            _context?.Entry(usaurio).CurrentValues.SetValues(obj);
            _context?.SaveChanges();
        }
        catch 
        {
            throw new Exception("Erro ao atualizar usuário!");
        }
    }
    public bool Delete(Usuario obj)
    {            
        try
        {
            var result = dataSet.SingleOrDefault(prop => prop.Id.Equals(obj.Id));
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