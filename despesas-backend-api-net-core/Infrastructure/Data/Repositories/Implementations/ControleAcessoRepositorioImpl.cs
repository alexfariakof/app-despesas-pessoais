using despesas_backend_api_net_core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using despesas_backend_api_net_core.Infrastructure.Security.Implementation;
using despesas_backend_api_net_core.Infrastructure.Security;

namespace despesas_backend_api_net_core.Infrastructure.Data.Repositories.Implementations
{
    public class ControleAcessoRepositorioImpl : IControleAcessoRepositorio
    {
        private readonly RegisterContext _context;
        private readonly ICrypto _crypto = Crypto.GetInstance;
        private readonly IEmailSender _emailSender;
        public ControleAcessoRepositorioImpl(RegisterContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        public bool Create(ControleAcesso controleAcesso)
        {
            if (FindByEmail(controleAcesso) != null)
                return false;
            
            
            DbSet<Usuario> dsUsuario = _context.Set<Usuario>();
            DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();
            DbSet<Categoria> dsCategoria = _context.Set<Categoria>();

            using (_context)
            {
                try
                {
                    controleAcesso.Senha = _crypto.Encrypt(controleAcesso.Senha);
                    dsUsuario.Add(controleAcesso.Usuario);
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
                        categoria.UsuarioId = controleAcesso.UsuarioId;
                        dsCategoria.Add(categoria);
                    }
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("ControleAcessoRepositorioImpl_Create_Exception", ex);
                }
            }
            return false;
        }
        public ControleAcesso FindByEmail(ControleAcesso controleAcesso)
        {
            var result = _context.ControleAcesso.SingleOrDefault(prop => prop.Login.Equals(controleAcesso.Login));
            return result;
        }
        public Usuario GetUsuarioByEmail(string email)
        {
            return _context.Usuario.SingleOrDefault(prop => prop.Email.Equals(email));
        }
        public bool RecoveryPassword(string email)
        {
            ControleAcesso controleAcesso = FindByEmail(new ControleAcesso { Login = email });
            controleAcesso.Usuario = GetUsuarioByEmail(email);

            using (_context)
            {
                DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();

                var result = dsControleACesso.SingleOrDefault(prop => prop.Id.Equals(controleAcesso.Id));
                try
                {
                    var senhaNova = Guid.NewGuid().ToString().Substring(0, 8);
                    controleAcesso.Senha = _crypto.Encrypt(senhaNova);
                    if (_emailSender.SendEmailPassword(controleAcesso.Usuario, senhaNova))
                    {
                        _context.Entry(result).CurrentValues.SetValues(controleAcesso);
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
        }
        public bool ChangePassword(int idUsuario, string password)
        {
            Usuario? usuario = _context.Usuario.SingleOrDefault(prop => prop.Id.Equals(idUsuario));
            
            if (usuario is null) 
                return false;

            ControleAcesso controleAcesso = FindByEmail(new ControleAcesso { Login = usuario.Email });
            DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();
                        
            try
            {
                var result = dsControleACesso.SingleOrDefault(prop => prop.Id.Equals(controleAcesso.Id));
                controleAcesso.Senha = _crypto.Encrypt(password);
                _context.Entry(result).CurrentValues.SetValues(controleAcesso);
                usuario.StatusUsuario = StatusUsuario.Ativo;
                _context.Entry(usuario).CurrentValues.SetValues(usuario);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("ChangePassword_Erro", ex);
            }
        }
        
        public bool isValidPasssword(ControleAcesso controleAcesso)
        {
            ControleAcesso _controleAcesso = FindByEmail(controleAcesso);
            
            if (_crypto.Decrypt(_controleAcesso.Senha).Equals(controleAcesso.Senha))
            {
                return true;
            }
            return false;
        }
    }
}