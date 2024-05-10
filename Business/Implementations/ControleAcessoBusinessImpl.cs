using Business.Abstractions;
using Business.Authentication;
using Business.Dtos.Core;
using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Entities;
using Repository.Persistency;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Business.Implementations;
public class ControleAcessoBusinessImpl<DtoCa, DtoLogin> : IControleAcessoBusiness<DtoCa, DtoLogin> where DtoCa : BaseControleAcessoDto where DtoLogin : BaseLoginDto, new()
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

    public void Create(DtoCa controleAcessoDto)
    {
        ControleAcesso controleAcesso = new ControleAcesso();
        controleAcesso.CreateAccount(new Usuario()
            .CreateUsuario(
            controleAcessoDto.Nome,
            controleAcessoDto.SobreNome,
            controleAcessoDto.Email,
            controleAcessoDto.Telefone,
            StatusUsuario.Ativo,
            PerfilUsuario.Usuario),
            controleAcessoDto.Email,
            controleAcessoDto.Senha
            );
        _repositorio.Create(controleAcesso);
    }

    public BaseAuthenticationDto ValidateCredentials(DtoLogin login)
    {
        ControleAcesso?  baseLogin = _repositorio.FindByEmail(new ControleAcesso { Login = login.Email });

        if (baseLogin == null)
            return AuthenticationException("Usuário inexistente!");                
        else if (baseLogin.Usuario.StatusUsuario == StatusUsuario.Inativo)
            return AuthenticationException("Usuário Inativo!");


        if (!_repositorio.IsValidPasssword(login.Email, login.Senha))
            return AuthenticationException("Senha inválida!");

        bool credentialsValid = baseLogin != null && login.Email == baseLogin.Login;
        if (credentialsValid)
        {
            baseLogin.RefreshToken = _tokenConfiguration.GenerateRefreshToken();
            baseLogin.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_tokenConfiguration.DaysToExpiry);         
            _repositorio.RefreshTokenInfo(baseLogin);
            return AuthenticationSuccess(baseLogin);
        }
        return AuthenticationException("Usuário Inválido!");
    }

    public BaseAuthenticationDto ValidateCredentials(string refreshToken)
    {
        var baseLogin = _repositorio.FindByRefreshToken(refreshToken);
        var credentialsValid = 
            baseLogin != null 
            && baseLogin.RefreshTokenExpiry >= DateTime.UtcNow
            && refreshToken.Equals(baseLogin.RefreshToken)
            && _tokenConfiguration.ValidateRefreshToken(refreshToken);

        if (credentialsValid)
            return AuthenticationSuccess(baseLogin);
        else if (baseLogin != null)
            this.RevokeToken(baseLogin.Id);

        return AuthenticationException("Refresh Token Inválido!");
    }
    public void RevokeToken(int idUsuario)
    {
        _repositorio.RevokeToken(idUsuario);
    }

    public void RecoveryPassword(string email)
    {
        IsValidEmail(email);

        var result = _repositorio.RecoveryPassword(email);
        ControleAcesso controleAcesso = _repositorio.FindByEmail(new ControleAcesso { Login = email });

        if (result && _emailSender.SendEmailPassword(controleAcesso.Usuario, Crypto.GetInstance.Decrypt(controleAcesso.Senha)))
            throw new ArgumentException("Usuário não encontrado!");
    }

    public void ChangePassword(int idUsuario, string password)
    {
        _repositorio.ChangePassword(idUsuario, password);
    }

    private BaseAuthenticationDto AuthenticationException(string message)
    {
        return new Business.Dtos.v2.AuthenticationDto
        {
            Authenticated = false,
            Message = message
        };
    }

    private BaseAuthenticationDto AuthenticationSuccess(ControleAcesso controleAcesso)
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

        return new Business.Dtos.v2.AuthenticationDto
        {
            Authenticated = true,
            Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
            Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
            AccessToken = token,
            RefreshToken = controleAcesso.RefreshToken,
            Message = "OK"
        };
    }

    private string IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email não pode ser em branco ou nulo!");

        if (email.Length > 256)
            throw new ArgumentException("Email inválido!");

        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);

        if (!regex.IsMatch(email))
            throw new ArgumentException("Email inválido!");

        return email;
    }
}