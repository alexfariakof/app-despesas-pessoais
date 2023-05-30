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

        public ControleAcessoRepositorioImpl(RegisterContext context)
        {
            _context = context;
        }

        public bool Create(ControleAcesso controleAcesso)
        {
            if (FindByEmail(controleAcesso) != null)
                return false;
            
            DbSet<Categoria> dsCategoria = _context.Set<Categoria>();
            DbSet<Usuario> dsUsuario = _context.Set<Usuario>();
            DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();

            using (_context)
            {
                try
                {
                    controleAcesso.Senha = Crypto.Encrypt(controleAcesso.Senha);
                    dsUsuario.Add(controleAcesso.Usuario);
                    dsControleACesso.Add(controleAcesso);

                    List<Categoria> lstCategoria = new List<Categoria>();
                    lstCategoria.Add(new Categoria
                    {                        
                        Descricao = "Alimentação",
                        IdTipoCategoria = 1,
                        TipoCategoria = TipoCategoria.Despesa
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Casa",
                        IdTipoCategoria = 1,
                        TipoCategoria = TipoCategoria.Despesa
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Serviços",
                        IdTipoCategoria = 1,
                        TipoCategoria = TipoCategoria.Despesa
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Saúde",
                        IdTipoCategoria = 1,
                        TipoCategoria = TipoCategoria.Despesa
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Imposto",
                        IdTipoCategoria = 1,
                        TipoCategoria = TipoCategoria.Despesa
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Transporte",
                        IdTipoCategoria = 1,
                        TipoCategoria = TipoCategoria.Despesa
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Lazer",
                        IdTipoCategoria = 1,
                        TipoCategoria = TipoCategoria.Despesa
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Outros",
                        IdTipoCategoria = 1,
                        TipoCategoria = TipoCategoria.Despesa
                    });

                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Salário",
                        IdTipoCategoria = 2,
                        TipoCategoria = TipoCategoria.Receita
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Prêmio",
                        IdTipoCategoria = 2,
                        TipoCategoria = TipoCategoria.Receita
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Investimento",
                        IdTipoCategoria = 2,
                        TipoCategoria = TipoCategoria.Receita
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Benefício",
                        IdTipoCategoria = 2,
                        TipoCategoria = TipoCategoria.Receita
                    });
                    lstCategoria.Add(new Categoria
                    {
                        Descricao = "Outros",
                        IdTipoCategoria = 2,
                        TipoCategoria = TipoCategoria.Receita
                    });
                    foreach (Categoria categoria in lstCategoria)
                    {
                        categoria.UsuarioId = controleAcesso.Usuario.Id;
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
                    controleAcesso.Senha = Crypto.Encrypt(senhaNova);
                    _context.Entry(result).CurrentValues.SetValues(controleAcesso);
                    _context.SaveChanges();
                    EnviarEmail(controleAcesso.Usuario, "<b>Nova senha:</b>" + senhaNova);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }

        public bool ChangePassword(int idUsuario, string password)
        {
            Usuario usuario = _context.Usuario.SingleOrDefault(prop => prop.Id.Equals(idUsuario));
            ControleAcesso controleAcesso = FindByEmail(new ControleAcesso { Login = usuario.Email });
            DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();

            var result = dsControleACesso.SingleOrDefault(prop => prop.Id.Equals(controleAcesso.Id));
            try
            {
                controleAcesso.Senha = Crypto.Encrypt(password);
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
        private void EnviarEmail(Usuario usuario, String message)
        {
            System.Net.Mail.SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("appdespesaspessoais@gmail.com", "roottoor");
            MailMessage mail = new MailMessage();
            mail.Sender = new System.Net.Mail.MailAddress("appdespesaspessoais@gmail.com", "App Despesas Pessoais");
            mail.From = new MailAddress("appdespesaspessoais@gmail.com", "App Despesas Pessoais");
            mail.To.Add(new MailAddress(usuario.Email, usuario.Nome + " " + usuario.SobreNome));
            mail.Subject = "Contato";
            mail.Body = " Mensagem do site:<br/> Prezado(a)   " + usuario.Nome + " " + usuario.SobreNome + "<br/>Segue dados para acesso a conta cadastrada.<br><b>E-mail:</b> " + usuario.Email + " <br/> " + message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            try
            {
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
            }
            catch 
            {
                throw new Exception("Erro ao enviar email!");
            }
            finally
            {
                mail = null;
            }
        }
        public bool isValidPasssword(ControleAcesso controleAcesso)
        {
            ControleAcesso _controleAcesso = FindByEmail(controleAcesso);
            
            if (Crypto.Decrypt(_controleAcesso.Senha).Equals(controleAcesso.Senha))
            {
                return true;
            }
            return false;
        }
    }
}
