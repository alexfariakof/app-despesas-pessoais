﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories;
using despesas_backend_api_net_core.Infrastructure.Security.Configuration;

namespace apiDespesasPessoais.Business.Implementations
{
    public class ControleAcessoBusinessImpl : IControleAcessoBusiness
    {
        private IControleAcessoRepositorio _repositorio;

        private SigningConfigurations _singingConfiguration;
        private TokenConfiguration _tokenConfiguration; 

        public ControleAcessoBusinessImpl(IControleAcessoRepositorio repositorio, SigningConfigurations singingConfiguration, TokenConfiguration tokenConfiguration)
        {
            _repositorio = repositorio;
            _singingConfiguration = singingConfiguration;
            _tokenConfiguration = tokenConfiguration;
        }

        public bool Create(ControleAcesso controleAcesso)
        {
            return _repositorio.Create(controleAcesso);
        }

        public object FindByLogin(ControleAcesso controleAcesso)
        {
            bool credentialsValid = false;

            if (_repositorio.GetUsuarioByEmail(controleAcesso.Login).StatusUsuario == StatusUsuario.Inativo)
                return ExceptionObject();

            if (controleAcesso != null && !string.IsNullOrWhiteSpace(controleAcesso.Login))
            {
                ControleAcesso baseLogin = _repositorio.FindByEmail(controleAcesso);
                credentialsValid = (baseLogin != null && controleAcesso.Login == baseLogin.Login && controleAcesso.Senha == baseLogin.Senha);
            }
            if(credentialsValid)
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

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, token, controleAcesso.Login);
            }
            else
            {
                return ExceptionObject();
            }            
        }

        public bool RecoveryPassword(string email)
        {
            return _repositorio.RecoveryPassword(email);
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            Microsoft.IdentityModel.Tokens.SecurityToken securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _singingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            string token = handler.WriteToken(securityToken);

            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                authenticated = false,
                message = "Falha durante a autenticação"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, string login)
        {
            Usuario usuario = _repositorio.GetUsuarioByEmail(login);
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK",
                usuario
            };
        }
    }
}