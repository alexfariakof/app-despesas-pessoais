global using Xunit;
global using Moq;
global using Bogus;
global using Domain.Entities;
global using Business.Implementations;
global using Repository.Persistency.Generic;
global using Repository.Persistency.Implementations;
global using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Core;
using Business.Authentication;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;

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
            .Setup(repo => repo.Get(It.IsAny<int>()))
            .Returns(
                (int id) =>
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
                (int id) =>
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

    public static string GenerateJwtToken(int userId)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();

        var signingConfigurations = new SigningConfigurations();
        configuration.GetSection("TokenConfigurations").Bind(signingConfigurations);

        var tokenConfigurations = new TokenConfiguration();
        configuration.GetSection("TokenConfigurations").Bind(tokenConfigurations);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingConfigurations.Key.ToString()));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[] { new Claim("IdUsuario", userId.ToString()) };
        var token = new JwtSecurityToken(
            issuer: tokenConfigurations.Issuer,
            audience: tokenConfigurations.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(tokenConfigurations.Seconds),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public static void SetupBearerToken(int idUsuario, ControllerBase controller)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
        };
        var identity = new ClaimsIdentity(claims, "IdUsuario");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers["Authorization"] = "Bearer " + Usings.GenerateJwtToken(idUsuario);
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }
}
