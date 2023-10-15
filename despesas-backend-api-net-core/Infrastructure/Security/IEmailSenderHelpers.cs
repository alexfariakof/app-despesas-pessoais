using despesas_backend_api_net_core.Domain.Entities;

internal interface IEmailSenderHelpers
{
    bool EnviarEmail(Usuario usuario, string message);
}