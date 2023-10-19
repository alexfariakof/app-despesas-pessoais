using despesas_backend_api_net_core.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Mail;

namespace despesas_backend_api_net_core.Infrastructure.Security.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly int _lengthPassword;
        private readonly string _hostSmpt;
        private readonly NetworkCredential _Credentials;
        public EmailSender()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                        
            int.TryParse(configuration.GetSection("EmailConfigurations:lengthPassword").Value, out _lengthPassword);
            _hostSmpt = configuration.GetSection("EmailConfigurations:host").Value;
            var login = configuration.GetSection("EmailConfigurations:login").Value;
            var senha = configuration.GetSection("EmailConfigurations:senha").Value;
            _Credentials = new NetworkCredential(login, senha);
        }

        private  void SendEmail(MailMessage message)
        {
            using (SmtpClient client = new SmtpClient(_hostSmpt, 587))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = _Credentials;

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    throw new Exception("EmailSender_SendEmail_Erro", ex);
                }
            }
        }
        public bool SendEmailPassword(Usuario usuario, string password)
        {
            try
            {
                if (usuario == null || usuario.Email.IsNullOrEmpty() || password.IsNullOrEmpty())
                    return false;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("appdespesaspessoais@gmail.com", "App Despesas Pessoais");
                    mail.To.Add(new MailAddress(usuario.Email, usuario.Nome + " " + usuario.SobreNome));
                    mail.Subject = "Contato Recuperação de senha de Acesso Despesas Pessoais";
                    mail.Body = $"Mensagem do site:<br/> Prezado(a) {usuario.Nome} {usuario.SobreNome}<br/>Segue dados para acesso a conta cadastrada.<br><b>Nova Senha :</b> {password}<br/>";
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    //SendEmail(mail);
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Enviar Email!", ex);
            }
        }
        
    }
}