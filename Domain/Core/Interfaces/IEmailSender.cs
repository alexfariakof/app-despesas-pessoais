using Domain.Entities;

namespace Domain.Core.Interfaces;
public interface IEmailSender
{
    bool SendEmailPassword(Usuario usuario, string senha);
}