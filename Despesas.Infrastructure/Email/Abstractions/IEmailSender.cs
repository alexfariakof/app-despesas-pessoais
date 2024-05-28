using Domain.Entities;

namespace Despesas.Infrastructure.Email.Abstractions;
public interface IEmailSender
{
    bool SendEmailPassword(Usuario usuario, string senha);
}