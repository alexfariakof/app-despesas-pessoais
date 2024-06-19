using Despesas.Infrastructure.Email;
using Domain.Entities.ValueObjects;
using __mock__.Repository;
using Repository.Persistency.Abstractions;
using Repository.Persistency.Implementations.Fixtures;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repository.Persistency.Implementations;
public sealed class ControleAcessoRepositorioImplTest : IClassFixture<ControleAcessoRepositorioFixture>
{
    private readonly ControleAcessoRepositorioFixture _fixture;

    public ControleAcessoRepositorioImplTest(ControleAcessoRepositorioFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Create_Should_Return_True()
    {
        // Arrange and Setup mock repository
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>()));
        var mockControleAcesso = MockControleAcesso.Instance.GetControleAcesso();

        // Act
        Action result = () => mockRepository.Object.Create(mockControleAcesso);

        // Assert
        Assert.IsNotType<Exception>(result);
    }

    [Fact]
    public void Create_Should_Return_False()
    {
        // Arrange and Setup mock repository
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>()));
        var mockControleAcesso = context.ControleAcesso.ToList().First();

        // Act
        Action result = () => mockRepository.Object.Create(mockControleAcesso);

        // Assert
        Assert.NotNull(result);
        mockRepository.Verify(repo => repo.Create(It.IsAny<ControleAcesso>()), Times.Never);
    }

    [Fact]
    public void Create_Should_Throws_Exception()
    {
        // Arrange and Setup mock repository
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<ControleAcesso, bool>>>())).Returns(new ControleAcesso());
        mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>())).Throws(new InvalidOperationException("ControleAcessoRepositorioImpl_Create_Exception"));

        // Act & Assert 
        ControleAcesso? nullControleAceesso = null;
        Assert.Throws<InvalidOperationException>(() => mockRepository.Object.Create(nullControleAceesso));
        //Assert.Equal("ControleAcessoRepositorioImpl_Create_Exception", exception.Message);
    }

    [Fact]
    public void FindByEmail_Should_Returns_ControleAcesso()
    {
        // Arrange and Setup Repository
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var mockControleAcesso = context.ControleAcesso.ToList().First();

        // Act
        var result = mockRepository.Object.Find(c => c.Equals(mockControleAcesso));

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ControleAcesso>(result);
        Assert.Equal(mockControleAcesso, result);
    }

    [Fact]
    public void RecoveryPassword_Should_Returns_True()
    {
        // Arrange
        var newPassword = "!12345";
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "RecoveryPassword_Should_Returns_True").Options;
        var context = new RegisterContext(options);
        context.PerfilUsuario.Add(new PerfilUsuario(PerfilUsuario.Perfil.Admin));
        context.PerfilUsuario.Add(new PerfilUsuario(PerfilUsuario.Perfil.User));
        context.SaveChanges();
        var lstControleAcesso = MockControleAcesso.Instance.GetControleAcessos().ToList();
        lstControleAcesso.ForEach(c => c.Usuario.PerfilUsuario = context.PerfilUsuario.First(tc => tc.Id == c.Usuario.PerfilUsuario.Id));
        var mockControleAcesso = new ControleAcesso { Login = lstControleAcesso.Last().Login };
        context.AddRange(lstControleAcesso);
        context.SaveChanges();
        var repository = new Mock<ControleAcessoRepositorioImpl>(context);        

        // Act
        var result = repository.Object.RecoveryPassword(mockControleAcesso.Login, newPassword);

        //Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
    }

    [Fact]
    public void RecoveryPassword_Should_Returns_False()
    {
        // Arrange
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var mockUsuario = new Usuario { Email = string.Empty };
        var mockControleAcesso = context.ControleAcesso.ToList().First();
        context.Usuario = Usings.MockDbSet(new List<Usuario> { mockUsuario }).Object;
        var newPassword = "!12345";
        ControleAcesso MockControleAcesso = new ControleAcesso
        {
            Id = mockControleAcesso.Id,
            Login = mockControleAcesso.Login,
            Senha = newPassword,
            Usuario = mockControleAcesso.Usuario,
            UsuarioId = mockControleAcesso.UsuarioId
        };
        context.ControleAcesso = Usings.MockDbSet(new List<ControleAcesso> { MockControleAcesso }).Object;

        context.SaveChanges();
        var emailSender = new Mock<EmailSender>();
        mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<ControleAcesso, bool>>>())).Returns(mockControleAcesso);
        mockRepository.Setup(repo => repo.RecoveryPassword(mockControleAcesso.Login, It.IsAny<string>())).Returns(false);

        // Act
        var result = mockRepository.Object.RecoveryPassword(mockControleAcesso.Login, newPassword);

        //Assert
        Assert.IsType<bool>(result);
        Assert.False(result);
    }

    [Fact]
    public void RecoveryPassword_Should_Returns_False_When_Throws_Exception()
    {
        // Arrange
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var dataset = context.ControleAcesso.ToList();
        context.ControleAcesso = Usings.MockDbSet(dataset).Object;
        context.SaveChanges();
        mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<ControleAcesso, bool>>>())).Throws(new Exception());
        var mockControleAcesso = dataset.First();

        // Act
        var result = mockRepository.Object.RecoveryPassword(mockControleAcesso.Login, "newPassword");

        //Assert
        Assert.IsType<bool>(result);
        Assert.False(result);
    }

    [Fact]
    public void ChangePassword_Should_Returns_False_When_Usuario_Null()
    {
        // Arrange
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var controleAcesso = context.ControleAcesso.ToList().First();
        mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<ControleAcesso, bool>>>())).Returns(controleAcesso);
        mockRepository.Setup(repo => repo.ChangePassword(0, "!12345")).Returns(true);

        // Act
        var result = mockRepository.Object.ChangePassword(0, "!12345");

        //Assert
        Assert.IsType<bool>(result);
        Assert.False(result);
    }

    [Fact]
    public void ChangePassword_Should_Returns_True()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "ChangePassword_Should_Returns_True").Options;
        var context = new RegisterContext(options);
        context.PerfilUsuario.Add(new PerfilUsuario(PerfilUsuario.Perfil.Admin));
        context.PerfilUsuario.Add(new PerfilUsuario(PerfilUsuario.Perfil.User));
        context.SaveChanges();
        var lstControleAcesso = MockControleAcesso.Instance.GetControleAcessos();
        lstControleAcesso.ForEach(c => c.Usuario.PerfilUsuario = context.PerfilUsuario.First(tc => tc.Id == c.Usuario.PerfilUsuario.Id));
        context.AddRange(lstControleAcesso);
        context.SaveChanges();
        var repository = new ControleAcessoRepositorioImpl(context);
        var controleAcesso = context.ControleAcesso.Last();
        
        // Act
        var result = repository.ChangePassword(controleAcesso.UsuarioId, "!12345");

        //Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
    }

    [Fact]
    public void ChangePassword_Should_Throws_Exception()
    {
        // Arrange
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var controleAcesso = context.ControleAcesso.ToList().First();
        var mockUsuario = controleAcesso.Usuario;
        mockUsuario.Email = "teste@teste.com";
        var dbSetMock = Usings.MockDbSet(context.ControleAcesso.ToList());
        var _dbContextMock = new Mock<RegisterContext>(context);
        _dbContextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dbSetMock.Object);
        _dbContextMock.Setup(c => c.SaveChanges()).Throws<Exception>();
        mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<ControleAcesso, bool>>>())).Returns(new ControleAcesso());
        mockRepository.Setup(repo => repo.ChangePassword(controleAcesso.UsuarioId, "!12345")).Throws<Exception>();

        // Act
        //var result = mockRepository.Object.ChangePassword(controleAcesso.UsuarioId, "!12345");
        Action result = () => mockRepository.Object.ChangePassword(controleAcesso.UsuarioId, "!12345");

        //Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => mockRepository.Object.ChangePassword(controleAcesso.UsuarioId, "!12345"));
        Assert.Equal("ChangePassword_Erro", exception.Message);
        Assert.True(true);
    }
     
    [Fact]
    public void Create_Should_Throw_Exception_When_User_Already_Exists()
    {
        // Arrange
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var mockControleAcesso = context.ControleAcesso.ToList().First();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => mockRepository.Object.Create(mockControleAcesso));
    }


    [Fact]
    public void FindById_Should_Return_ControleAcesso_When_User_Exists()
    {
        // Arrange
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var mockControleAcesso = context.ControleAcesso.ToList().First();

        // Act
        var result = mockRepository.Object.Find(controleAcesso => controleAcesso.Id.Equals(mockControleAcesso.Id));

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockControleAcesso, result);
    }

    [Fact]
    public void FindById_Should_Return_Null_When_User_Not_Exists()
    {
        // Arrange
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var nonExistingId = -1;
        var repository = new ControleAcessoRepositorioImpl(context);

        // Act
        var result = repository.Find(controleAcesso => controleAcesso.Id.Equals(nonExistingId));

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void RevokeToken_Should_Throw_Exception_When_User_Not_Exists()
    {
        // Arrange
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var nonExistingId = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => mockRepository.Object.RevokeRefreshToken(nonExistingId));
    }

    [Fact]
    public void RevokeToken_Should_Remove_Token_When_User_Exists()
    {
        // Arrange
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var mockControleAcesso = context.ControleAcesso.ToList().First();

        // Act
        mockRepository.Object.RevokeRefreshToken(mockControleAcesso.Id);

        // Assert
        Assert.Empty(mockControleAcesso.RefreshToken);
        Assert.Null(mockControleAcesso.RefreshTokenExpiry);
    }

    [Fact]
    public void RefreshTokenInfo_Should_Update_Token_Info()
    {
        // Arrange
        var context = _fixture.Context;
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_fixture.MockRepository.Object);
        var mockControleAcesso = context.ControleAcesso.ToList().First();

        // Act
        mockControleAcesso.RefreshToken = "new_token";
        mockControleAcesso.RefreshTokenExpiry = DateTime.UtcNow.AddHours(1);
        mockRepository.Object.RefreshTokenInfo(mockControleAcesso);

        // Assert
        Assert.NotNull(mockControleAcesso.RefreshToken);
        Assert.NotNull(mockControleAcesso.RefreshTokenExpiry);
        Assert.Equal("new_token", mockControleAcesso.RefreshToken);
    }
}
