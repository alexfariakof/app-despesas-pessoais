using System.Net;
using System.Net.Mail;
using Despesas.Infrastructure.Email.Abstractions;
using Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Despesas.Infrastructure.Email;
public class EmailSender : IEmailSender
{
    private readonly string? _hostSmpt;
    private readonly NetworkCredential? _Credentials;
    public EmailSender()
    {
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

        if (File.Exists(jsonFilePath))
        {
            var jsonContent = File.ReadAllText(jsonFilePath);
            var config = JObject.Parse(jsonContent);

            _hostSmpt = config["EmailConfigurations"]?["host"]?.ToString();
            var login = config["EmailConfigurations"]?["login"]?.ToString();
            var senha = config["EmailConfigurations"]?["senha"]?.ToString();
            _Credentials = new NetworkCredential(login, senha);
        }        
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
            catch
            {
                throw new ArgumentException("EmailSender_SendEmail_Erro");
            }
        }
    }
    public bool SendEmailPassword(Usuario usuario, string password)
    {
        try
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(password))
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
        catch
        {
            throw new ArgumentException("Erro ao Enviar Email!");
        }
    }    
}