global using Domain.Entities;
global using Moq;
global using Xunit;
using AutoMapper;
using Business.Authentication;
using Despesas.Business.Authentication.Abstractions;
using Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Persistency.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class Usings
{
    private Usings() { }

    public static Mock<DbSet<T>> MockDbSet<T>(List<T> data, DbContext? context = null)
        where T : class
    {
        var queryableData = data.AsQueryable();
        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        dbSetMock
            .As<IQueryable<T>>()
            .Setup(m => m.GetEnumerator())
            .Returns(() => queryableData.GetEnumerator());
        dbSetMock.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(data.Add);
        dbSetMock
            .Setup(d => d.Update(It.IsAny<T>()))
            .Callback<T>(item =>
            { // Atualizar a entidade no contexto
                var existingItem = data.FirstOrDefault(i => i == item);
                if (existingItem != null)
                {
                    context.Attach(item);

                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChangesAsync().Wait();
                }
            });
        dbSetMock.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(item => data.Remove(item));
        return dbSetMock;
    }

    public static Mock<IRepositorio<T>> MockRepositorio<T>(List<T> _dataSet)
        where T : BaseModel
    {
        var _mock = new Mock<IRepositorio<T>>();

        _mock
            .Setup(repo => repo.Get(It.IsAny<Guid>()))
            .Returns(
                (Guid id) =>
                {
                    return _dataSet.Single(item => item.Id == id);
                }
            );

        _mock.Setup(repo => repo.GetAll()).Returns(() => _dataSet.ToList());
        _mock.Setup(repo => repo.Insert(ref It.Ref<T>.IsAny));
        _mock
            .Setup(repo => repo.Update(ref It.Ref<T>.IsAny));
        _mock
            .Setup(repo => repo.Delete(It.IsAny<T>()))
            .Returns(
                (Guid id) =>
                {
                    var itemToRemove = _dataSet.FirstOrDefault(item => item.Id == id);
                    if (itemToRemove != null)
                    {
                        _dataSet.Remove(itemToRemove);
                        return true;
                    }

                    return false;
                }
            );
        return _mock;
    }

    public static string GenerateJwtToken(Guid userId)
    {
        var options = Options.Create(new TokenOptions
        {
            Issuer = "XUnit-Issuer",
            Audience = "XUnit-Audience",
            Seconds = 3600,
            DaysToExpiry = 1,
            Certificate= "certificate/webapi-cert.pfx",
            Password= "12345T!"
        });

        string certificatePath = Path.Combine(AppContext.BaseDirectory, options.Value.Certificate);
        X509Certificate2 certificate = new X509Certificate2(certificatePath, options.Value.Password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);


        var signingConfigurations = new SigningConfigurations(certificate, options);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingConfigurations.Key.ToString()));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[] { new Claim("sub", userId.ToString()) };
        var token = new JwtSecurityToken(
            issuer: signingConfigurations.TokenConfiguration.Issuer,
            audience: signingConfigurations.TokenConfiguration.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(signingConfigurations.TokenConfiguration.Seconds),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public static void SetupBearerToken(Guid idUsuario, ControllerBase controller)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
        };
        var identity = new ClaimsIdentity(claims, "sub");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers["Authorization"] = "Bearer " + Usings.GenerateJwtToken(idUsuario);
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }
}
