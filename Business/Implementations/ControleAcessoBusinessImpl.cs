using AutoMapper;
using Business.Abstractions;
using Business.Authentication;
using Business.Dtos.Core;
using Cryptography;
using Domain.Core.Interfaces;
using Domain.Entities;
using Repository.Persistency.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Business.Implementations;
public class ControleAcessoBusinessImpl<DtoCa, DtoLogin> : IControleAcessoBusiness<DtoCa, DtoLogin> where DtoCa : ControleAcessoDtoBase where DtoLogin : LoginDtoBase, new()
{
    private readonly IMapper _mapper;
    private readonly IControleAcessoRepositorioImpl _repositorio;
    private readonly IEmailSender _emailSender;
    private readonly SigningConfigurations _singingConfiguration;

    public ControleAcessoBusinessImpl(IMapper mapper, IControleAcessoRepositorioImpl repositorio, SigningConfigurations singingConfiguration, IEmailSender emailSender)
    {
        _mapper = mapper;
        _repositorio = repositorio;
        _singingConfiguration = singingConfiguration;
        _emailSender = emailSender;
    }

    public void Create(DtoCa controleAcessoDto)
    {
        var usuario = _mapper.Map<Usuario>(controleAcessoDto);
        usuario = new Usuario().CreateUsuario(usuario);
        ControleAcesso controleAcesso = new ControleAcesso();
        controleAcesso.CreateAccount(usuario, controleAcessoDto.Senha ?? "");
        _repositorio.Create(controleAcesso);
    }

    public AuthenticationDto ValidateCredentials(DtoLogin loginDto)
    {
        ControleAcesso baseLogin = _repositorio.Find(c => c.Login.Equals(loginDto.Email)) ?? throw new ArgumentException("Usuário inexistente!");

        if (baseLogin?.Usuario?.StatusUsuario == StatusUsuario.Inativo)
            return AuthenticationException("Usuário Inativo!");

        if (!Crypto.Instance.IsEquals(loginDto?.Senha ?? "", baseLogin?.Senha ?? ""))
            return AuthenticationException("Senha inválida!");

        bool credentialsValid = baseLogin is not null && loginDto?.Email == baseLogin.Login;
        if (credentialsValid && baseLogin is not null)
        {
            baseLogin.RefreshToken =  _singingConfiguration.TokenConfiguration.GenerateRefreshToken();
            baseLogin.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_singingConfiguration.TokenConfiguration.DaysToExpiry);         
            _repositorio.RefreshTokenInfo(baseLogin);
            return AuthenticationSuccess(baseLogin);
        }
        return AuthenticationException("Usuário Inválido!");
    }

    public AuthenticationDto ValidateCredentials(string refreshToken)
    {
        var baseLogin = _repositorio.FindByRefreshToken(refreshToken);
        var credentialsValid = 
            baseLogin is not null 
            && baseLogin.RefreshTokenExpiry >= DateTime.UtcNow
            && refreshToken.Equals(baseLogin.RefreshToken)
            && _singingConfiguration.TokenConfiguration.ValidateRefreshToken(refreshToken);

        if (credentialsValid && baseLogin is not null)
            return AuthenticationSuccess(baseLogin);
        else if (baseLogin is not null)
            this.RevokeToken(baseLogin.Id);

        return AuthenticationException("Refresh Token Inválido!");
    }

    public void RevokeToken(int idUsuario)
    {
        _repositorio.RevokeRefreshToken(idUsuario);
    }

    public void RecoveryPassword(string email)
    {
        IsValidEmail(email);
        var newPassword = Guid.NewGuid().ToString().Substring(0, 8);
        var result = _repositorio.RecoveryPassword(email, newPassword);
        var controleAcesso = _repositorio.Find(c => c.Login.Equals(email));

        if (result && _emailSender.SendEmailPassword(controleAcesso?.Usuario, newPassword))
            throw new ArgumentException("Erro ao enviar email de recuperação de senha!");
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

        ClaimsIdentity identity = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim("IdUsuario",  controleAcesso.UsuarioId.ToString())
        });

        DateTime createDate = DateTime.Now;
        DateTime expirationDate = createDate + TimeSpan.FromSeconds(_singingConfiguration.TokenConfiguration.Seconds);        
        string token = _singingConfiguration.CreateAccessToken(identity);

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

    private static void IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email não pode ser em branco ou nulo!");

        if (email.Length > 256)
            throw new ArgumentException("Email inválido!");

        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);

        if (!regex.IsMatch(email))
            throw new ArgumentException("Email inválido!");
    }
}