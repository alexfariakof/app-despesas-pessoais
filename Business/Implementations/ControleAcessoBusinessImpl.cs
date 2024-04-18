using Business.Abstractions;
using Business.Authentication;
using Business.Dtos;
using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Entities;
using Repository.Persistency;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Business.Implementations;
public class ControleAcessoBusinessImpl : IControleAcessoBusiness
{
    private readonly IControleAcessoRepositorioImpl _repositorio;
    private readonly IEmailSender _emailSender;
    private readonly SigningConfigurations _singingConfiguration;
    private readonly TokenConfiguration _tokenConfiguration;   

    public ControleAcessoBusinessImpl(IControleAcessoRepositorioImpl repositorio, SigningConfigurations singingConfiguration, TokenConfiguration tokenConfiguration, IEmailSender emailSender)
    {
        _repositorio = repositorio;
        _singingConfiguration = singingConfiguration;
        _tokenConfiguration = tokenConfiguration;
        _emailSender = emailSender;
    }

    public void Create(ControleAcesso controleAcesso)
    {
        _repositorio.Create(controleAcesso);
    }

    public AuthenticationDto ValidateCredentials(ControleAcessoDto controleAcesso)
    {
        ControleAcesso?  baseLogin = _repositorio.FindByEmail(new ControleAcesso { Login = controleAcesso.Email });

        if (baseLogin == null)
            return AuthenticationException("Usuário inexistente!");                
        else if (baseLogin.Usuario.StatusUsuario == StatusUsuario.Inativo)
            return AuthenticationException("Usuário Inativo!");


        if (!_repositorio.IsValidPasssword(controleAcesso.Email, controleAcesso.Senha))
            return AuthenticationException("Senha inválida!");

        bool credentialsValid = baseLogin != null && controleAcesso.Email == baseLogin.Login;
        if (credentialsValid)
        {
            baseLogin.RefreshToken = _tokenConfiguration.GenerateRefreshToken();
            baseLogin.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_tokenConfiguration.DaysToExpiry);         
            _repositorio.RefreshTokenInfo(baseLogin);
            return AuthenticationSuccess(baseLogin);
        }
        return AuthenticationException("Usuário Inválido!");
    }

    public AuthenticationDto ValidateCredentials(AuthenticationDto authenticationDto,  int idUsuario)
    {
        var baseLogin = _repositorio.FindById(idUsuario);
        var credentialsValid = 
            baseLogin != null 
            && baseLogin.RefreshTokenExpiry >= DateTime.UtcNow
            && authenticationDto.RefreshToken.Equals(baseLogin.RefreshToken)
            && _tokenConfiguration.ValidateRefreshToken(authenticationDto.RefreshToken);

        if (credentialsValid)
            return AuthenticationSuccess(baseLogin);
        else
            this.RevokeToken(idUsuario);

        return AuthenticationException("Token Inválido!");
    }
    public void RevokeToken(int idUsuario)
    {
        _repositorio.RevokeToken(idUsuario);
    }

    public void RecoveryPassword(string email)
    {        
        var result = _repositorio.RecoveryPassword(email);
        ControleAcesso controleAcesso = _repositorio.FindByEmail(new ControleAcesso { Login = email });

        if (result && _emailSender.SendEmailPassword(controleAcesso.Usuario, Crypto.GetInstance.Decrypt(controleAcesso.Senha)))
            throw new ArgumentException("Usuário não encontrado  ");
    }

    public void ChangePassword(int idUsuario, string password)
    {
        _repositorio.ChangePassword(idUsuario, password);
    }

    private AuthenticationDto AuthenticationException(string message)
    {
        return new AuthenticationDto
        {
            Authenticated = false,
            Message = message
        };
    }

    private AuthenticationDto AuthenticationSuccess(ControleAcesso controleAcesso)
    {

        ClaimsIdentity identity = new ClaimsIdentity(
            new GenericIdentity(controleAcesso.Login, "Login"),
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, controleAcesso.Login)
            });

        DateTime createDate = DateTime.Now;
        DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);        
        string token = _singingConfiguration.GenerateAccessToken(identity, _tokenConfiguration, controleAcesso.Usuario.Id);

        return new AuthenticationDto
        {
            Authenticated = true,
            Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
            Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
            AccessToken = token,
            RefreshToken = controleAcesso.RefreshToken,
            Message = "OK"
        };
    }
}