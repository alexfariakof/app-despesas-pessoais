using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VO;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using despesas_backend_api_net_core.Infrastructure.Data.Common;

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
            DbSet<Usuario> dsUsuario = null;

            using (_context)
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        dsUsuario.Add(controleAcesso.Usuario);

                        _context.SaveChanges();

                        string sql = "INSERT INTO[dbo].[ControleAcesso] ([login], [senha], [idUsuario]) VALUES ({0}, {1}, {2})";
                        _context.Database.ExecuteSqlRaw(sql, controleAcesso.Usuario.Email, controleAcesso.Senha, controleAcesso.IdUsuario);

                        dbContextTransaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }

            }
            return false;
        }

        public ControleAcesso FindByEmail(ControleAcesso controleAcesso)
        {
            return _context.ControleAcesso.SingleOrDefault(prop => prop.Login.Equals(controleAcesso.Login));
        }

        public Usuario GetUsuarioByEmail(string login)
        {
            return _context.Usuario.SingleOrDefault(prop => prop.Email.Equals(login));
        }

        public bool RecoveryPassword(string email)
        {
            Usuario usuario = GetUsuarioByEmail(email);
            if (usuario == null)
                return false;

            using (_context)
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        string sql = "UPDATE ControleAcesso SET senha = {0} WHERE login = {1}";

                        var senhaNova = Guid.NewGuid().ToString().Substring(0,8);

                        _context.Database.ExecuteSqlRaw(sql, senhaNova, usuario.Email);

                        EnviarEmail(usuario, "<b>Nova senha:</b>" + senhaNova);
                        
                        dbContextTransaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
            return false;
        }

        private void EnviarEmail(Usuario usuario, String message)
        {
            System.Net.Mail.SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("appdespesaspessoais@gmail.com", "roottoor");
            MailMessage mail = new MailMessage();
            mail.Sender = new System.Net.Mail.MailAddress("appdespesaspessoais@gmail.com", "App Despesas Pessoais");
            mail.From = new MailAddress("appdespesaspessoais@gmail.com", "App Despesas Pessoais");
            mail.To.Add(new MailAddress(usuario.Email, usuario.Nome + " " + usuario.sobreNome));
            mail.Subject = "Contato";
            mail.Body = " Mensagem do site:<br/> Prezado(a)   " + usuario.Nome + " " + usuario.sobreNome + "<br/>Segue dados para acesso a conta cadastrada.<br><b>E-mail:</b> " + usuario.Email + " <br/> " + message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                //client.Send(mail);
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                mail = null;
            }
        }
    }
}
