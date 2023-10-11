global using Xunit;
global using Moq;
global using despesas_backend_api_net_core.Domain.Entities;
global using despesas_backend_api_net_core.Domain.VM;
global using despesas_backend_api_net_core.Infrastructure.Data.Common;
global using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Implementations;
global using Microsoft.EntityFrameworkCore;
global using despesas_backend_api_net_core.XUnit.Fakers;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using despesas_backend_api_net_core.Infrastructure.Security.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public static class Usings
{
    public static Mock<DbSet<T>> MockDbSet<T>(List<T> data) where T : class
    {
        var queryableData = data.AsQueryable();
        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());
        dbSetMock.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(data.Add);
        dbSetMock.Setup(d => d.Update(It.IsAny<T>())).Callback<T>(item =>
        {
            var index = data.IndexOf(item);
            if (index != -1)
            {
                data[index] = item;
            }
        });
        dbSetMock.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(item => data.Remove(item));
        return dbSetMock;
    }

    public static Mock<IRepositorio<T>> MockRepositorio<T>(List<T> _dataSet) where T : BaseModel
    {
        var _mock = new Mock<IRepositorio<T>>();
        _mock.Setup(repo => repo.Get(It.IsAny<int>())).Returns((int id) => { return _dataSet.SingleOrDefault(item => item.Id == id); });
        _mock.Setup(repo => repo.GetAll()).Returns(() => _dataSet.ToList());
        _mock.Setup(repo => repo.Insert(It.IsAny<T>())).Returns((T item) => item);
        _mock.Setup(repo => repo.Update(It.IsAny<T>())).Returns((T updatedItem) =>
        {
            var existingItem = _dataSet.FirstOrDefault(item => item.Id == updatedItem.Id);
            if (existingItem != null)
            {
                existingItem = updatedItem;

            }
            return updatedItem;
        });
        _mock.Setup(repo => repo.Delete(It.IsAny<T>())).Returns((int id) =>
        {
            var itemToRemove = _dataSet.FirstOrDefault(item => item.Id == id);
            if (itemToRemove != null)
            {
                _dataSet.Remove(itemToRemove);
                return true;
            }

            return false;
        });
        return _mock;
    }
    public static string GenerateJwtToken(int userId)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var signingConfigurations = new SigningConfigurations();
        configuration.GetSection("TokenConfigurations").Bind(signingConfigurations);

        var tokenConfigurations = new TokenConfiguration();
        configuration.GetSection("TokenConfigurations").Bind(tokenConfigurations);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingConfigurations.Key.ToString()));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim("IdUsuario", userId.ToString())
            };

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

}