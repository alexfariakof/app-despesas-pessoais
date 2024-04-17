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
        bool credentialsValid = false;
        ControleAcesso? baseLogin = null;

        var usuario = _repositorio.GetUsuarioByEmail(controleAcesso.Email);
        if (usuario == null)
            return ExceptionObject("Usuário inexistente!");
        else if (usuario.StatusUsuario == StatusUsuario.Inativo)
            return ExceptionObject("Usuário Inativo!");

        if (!string.IsNullOrWhiteSpace(controleAcesso.Email))
        {
            baseLogin = _repositorio.FindByEmail( new ControleAcesso { Login = controleAcesso.Email });
            if (baseLogin == null)
                return ExceptionObject("Email inexistente!");
            if (!_repositorio.IsValidPasssword(controleAcesso.Email, controleAcesso.Senha))
                return ExceptionObject("Senha inválida!");

            credentialsValid = baseLogin != null && controleAcesso.Email == baseLogin.Login;
        }

        if (credentialsValid)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(controleAcesso.Email, "Login"),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, controleAcesso.Email)
                });

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
            string token = CreateToken(identity, createDate, expirationDate, usuario.Id);
            baseLogin.RefreshToken = CreateRefreshToken();
            baseLogin.RefreshTokenExpiry = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry);
            
            _repositorio.RefreshTokenInfo(baseLogin);

            return SuccessObject(createDate, expirationDate, token, controleAcesso.Email, baseLogin.RefreshToken);
        }
        return ExceptionObject("Usuário Inválido!");
    }

    public AuthenticationDto ValidateCredentials(AuthenticationDto authenticationDto, int idUsuario)
    {
        var baseLogin = _repositorio.FindById(idUsuario);
        var credentialsValid = baseLogin != null && baseLogin.RefreshTokenExpiry >= DateTime.Now;

        if (credentialsValid)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(baseLogin.Login, "Login"),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, baseLogin.Login)
                });

            baseLogin.RefreshToken = CreateRefreshToken();
            baseLogin.RefreshTokenExpiry = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry);

            _repositorio.RefreshTokenInfo(baseLogin);
            authenticationDto.RefreshToken = baseLogin.RefreshToken;
            return authenticationDto;
        }
        return ExceptionObject("Token Inválido!");
    }

    public bool RevokeToken(int idUsuario)
    {
        return _repositorio.RevokeToken(idUsuario);
    }

    public bool RecoveryPassword(string email)
    {        
        var result = _repositorio.RecoveryPassword(email);
        ControleAcesso controleAcesso = _repositorio.FindByEmail(new ControleAcesso { Login = email });

        if (result && _emailSender.SendEmailPassword(controleAcesso.Usuario, Crypto.GetInstance.Decrypt(controleAcesso.Senha)))
            return true;

        return false;
    }
    public bool ChangePassword(int idUsuario, string password)
    {
        return _repositorio.ChangePassword(idUsuario, password);
    }

    private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, int idUsuario)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        Microsoft.IdentityModel.Tokens.SecurityToken securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
        {
            Issuer = _tokenConfiguration.Issuer,
            Audience = _tokenConfiguration.Audience,
            SigningCredentials = _singingConfiguration.SigningCredentials,
            Subject = identity,
            NotBefore = createDate,
            Expires = expirationDate,
            Claims = new Dictionary<string, object> { { "IdUsuario", idUsuario } },
        });

        string token = handler.WriteToken(securityToken);        
        return token;
    }

    private string CreateRefreshToken()
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        Microsoft.IdentityModel.Tokens.SecurityToken securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor());
        return handler.WriteToken(securityToken);
    }

    private AuthenticationDto ExceptionObject(string message)
    {
        return new AuthenticationDto
        {
            Authenticated = false,
            Message = message
        };
    }

    private AuthenticationDto SuccessObject(DateTime createDate, DateTime expirationDate, string token, string login, string refreshToken)
    {
        Usuario usuario = _repositorio.GetUsuarioByEmail(login);
        return new AuthenticationDto
        {
            Authenticated = true,
            Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
            Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
            AccessToken = token,
            RefreshToken = refreshToken,
            Message = "OK"
        };
    }    
}