using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VO;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

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
            //Como Esta utilizando dados em memoria BeginTransaction não é aceito 
            DbSet<Usuario> dsUsuario = _context.Set<Usuario>();
            DbSet<ControleAcesso> dsControleACesso = _context.Set<ControleAcesso>();

            using (_context)
            {
                
                //using (var dbContextTransaction = _context.Database.BeginTransaction())
                //{
                    try
                    {
                        dsUsuario.Add(controleAcesso.Usuario);                    
                        dsControleACesso.Add(controleAcesso);
                        _context.SaveChanges();

                    /*                                       
                        dbContextTransaction.Commit();
                    */
                    return true;
                    }
                    catch (Exception)
                    {
                        //dbContextTransaction.Rollback();
                    }
                //}

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
            ControleAcesso controleAcesso = FindByEmail(new ControleAcesso { Login = email});
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
                    controleAcesso.Senha = senhaNova;
                    _context.Entry(result).CurrentValues.SetValues(controleAcesso);
                    _context.SaveChanges();

                    var resultUsuario = this.GetUsuarioByEmail(controleAcesso.Login);
                    var dataSet = _context.Set<Usuario>();
                    Usuario usaurio = new Usuario
                    {
                        Id = controleAcesso.IdUsuario,
                        StatusUsuario = StatusUsuario.Ativo
                    };
                    _context.Entry(resultUsuario).CurrentValues.SetValues(usaurio);
                    _context.SaveChanges();                    
                    EnviarEmail(controleAcesso.Usuario, "<b>Nova senha:</b>" + senhaNova);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                /*
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
                */
            }
            return false;
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
