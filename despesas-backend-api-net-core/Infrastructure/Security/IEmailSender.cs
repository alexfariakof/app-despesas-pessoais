using despesas_backend_api_net_core.Domain.Entities;
using System.Net.Mail;

namespace despesas_backend_api_net_core.Infrastructure.Security
{
    public interface IEmailSender
    {
        bool SendEmailPassword(Usuario usuario, string senha);
    }
}
