using Domain.Entities;

namespace Domain.Core.Interface;
public interface IEmailSender
{
    bool SendEmailPassword(Usuario usuario, string senha);
}