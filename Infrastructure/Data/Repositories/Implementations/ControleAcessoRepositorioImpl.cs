using despesas_backend_api_net_core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using System.Net;
using despesas_backend_api_net_core.Infrastructure.Security.Configuration;

namespace despesas_backend_api_net_core.Infrastructure.Data.Repositories.Implementations
{
    public class ControleAcessoRepositorioImpl : IControleAcessoRepositorio
    {
        private readonly RegisterContext _context;
        private readonly Crypto _crypto = Crypto.GetInstance;
        public ControleAcessoRepositorioImpl(RegisterContext context)
        {
            _context = context;
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
                        dsCategoria.Add(categoria);
                    }
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }
        public ControleAcesso FindByEmail(ControleAcesso controleAcesso)
        {
            return _context.ControleAcesso.SingleOrDefault(prop => prop.Login.Equals(controleAcesso.Login));
        }
        public Usuario GetUsuarioByEmail(string email)
        {
            return _context.Usuario.SingleOrDefault(prop => prop.Email.Equals(email));
        }
        public bool RecoveryPassword(string email)
        {
            ControleAcesso controleAcesso = FindByEmail(new ControleAcesso { Login = email });
            controleAcesso.Usuario = GetUsuarioByEmail(email);

            if (controleAcesso == null)
                return false;

            using (_context)
            {
                DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();

                var result = dsControleACesso.SingleOrDefault(prop => prop.Id.Equals(controleAcesso.Id));
                try
                {
                    var senhaNova = Guid.NewGuid().ToString().Substring(0, 8);
                    controleAcesso.Senha = _crypto.Encrypt(senhaNova);
                    if (EnviarEmail(controleAcesso, "<b>Nova senha:</b>" + senhaNova))
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
            Usuario usuario = _context.Usuario.SingleOrDefault(prop => prop.Id.Equals(idUsuario));
            ControleAcesso controleAcesso = FindByEmail(new ControleAcesso { Login = usuario.Email });
            DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();

            var result = dsControleACesso.SingleOrDefault(prop => prop.Id.Equals(controleAcesso.Id));
            try
            {
                controleAcesso.Senha = _crypto.Encrypt(password);
                _context.Entry(result).CurrentValues.SetValues(controleAcesso);
                usuario.StatusUsuario = StatusUsuario.Ativo;
                _context.Entry(usuario).CurrentValues.SetValues(usuario);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool EnviarEmail(ControleAcesso controleAcesso, string message)
        {
            Usuario usuario = controleAcesso.Usuario;

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("appdespesaspessoais@gmail.com", "@Toor01!");

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("appdespesaspessoais@gmail.com", "App Despesas Pessoais");
                    mail.To.Add(new MailAddress(usuario.Email, usuario.Nome + " " + usuario.SobreNome));
                    mail.Subject = "Contato";
                    mail.Body = $"Mensagem do site:<br/> Prezado(a) {usuario.Nome} {usuario.SobreNome}<br/>Segue dados para acesso a conta cadastrada.<br><b>Nova Senha :</b> {controleAcesso.Senha}<br/>{message}";
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    try
                    {
                        client.Send(mail);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
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