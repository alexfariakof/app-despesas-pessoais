using Business.Dtos.Core;

namespace Business.Dtos.v1;
public class AuthenticationDto : BaseAuthenticationDto
{
    public bool Authenticated { get; set; }
    public string? Created { get; set; }
    public string? Expiration { get; set; }
    public string? AccessToken { get; set; }
    public string? Message { get; set; }
}