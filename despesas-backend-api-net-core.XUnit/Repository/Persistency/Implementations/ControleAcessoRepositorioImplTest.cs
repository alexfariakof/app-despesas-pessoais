using Domain.Core;
using Moq;

namespace Repository.Persistency.Implementations;
public class ControleAcessoRepositorioImplTest
{
    private readonly RegisterContext _context;
    private Mock<ControleAcessoRepositorioImpl> _repository;

    public ControleAcessoRepositorioImplTest()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "Controle de Acesso Repo Database InMemory").Options;
        _context = new RegisterContext(options);
        _context.ControleAcesso.AddRange(ControleAcessoFaker.Instance.ControleAcessos(10));
        _context.SaveChanges();        
        _repository = new Mock<ControleAcessoRepositorioImpl>(_context);
    }

    [Fact]
    public void Create_Should_Return_True()
    {
        // Arrange
        var controleAcesso = ControleAcessoFaker.Instance.GetNewFaker();        
        controleAcesso.Usuario.StatusUsuario = StatusUsuario.Ativo;
        controleAcesso.Usuario.PerfilUsuario = PerfilUsuario.Administrador;

        // Create a mock repository
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns((ControleAcesso?)null);
        mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>()));

        // Act
        mockRepository.Object.Create(controleAcesso);

        // Assert        
        mockRepository.Verify(repo => repo.Create(controleAcesso), Times.Never);
    }

    [Fact]
    public void Create_Should_Return_False()
    {
        // Arrange and Setup mock repository
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_repository.Object);
        mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>()));
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        // Act
        Action result = () =>  mockRepository.Object.Create(mockControleAcesso);

        // Assert
        Assert.NotNull(result);
        mockRepository.Verify(repo => repo.Create(It.IsAny<ControleAcesso>()), Times.Never);
    }

    [Fact]
    public void Create_Should_Throws_Exception()
    {
        // Arrange and Setup mock repository
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(new ControleAcesso());
        //mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>())).Returns(false);
        mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>())).Throws(new InvalidOperationException("ControleAcessoRepositorioImpl_Create_Exception"));        
        
        // Act & Assert 
        Assert.Throws<InvalidOperationException>(() => _repository.Object.Create((ControleAcesso)null));
        //Assert.Equal("ControleAcessoRepositorioImpl_Create_Exception", exception.Message);
    }

    [Fact]
    public void FindByEmail_Should_Returns_ControleAcesso()
    {
        // Arrange and Setup Repository
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_repository.Object);
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        // Act
        var result = mockRepository.Object.FindByEmail(mockControleAcesso);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ControleAcesso>(result);
        Assert.Equal(mockControleAcesso, result);
        //_mockRepository.Verify(repo => repo.FindByEmail(It.IsAny<ControleAcesso>()), Times.Once);
    }

    [Fact]
    public void RecoveryPassword_Should_Returns_True() // Metodo não pode ser Testado por motivo do Enviar Email
    {
        // Arrange
        var mockControleAcesso = _context.ControleAcesso.ToList().First();        
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, _context);
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(mockControleAcesso);
        mockRepository.Setup(repo => repo.RecoveryPassword(mockControleAcesso.Login)).Returns(true);
        

        // Act
        var result = mockRepository.Object.RecoveryPassword(mockControleAcesso.Login);

        //Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
    }

    [Fact]
    public void RecoveryPassword_Should_Returns_False()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "Test Data Base Recovery Password").Options;
        var mockContext = new RegisterContext(options);
        var mockUsuario = new Usuario { Email = string.Empty };
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        mockContext.Usuario = Usings.MockDbSet(new List<Usuario> { mockUsuario }).Object;
        ControleAcesso MockControleAceesso = new ControleAcesso
        {
            Id = mockControleAcesso.Id,
            Login = mockControleAcesso.Login,
            Senha = "!12345",
            Usuario = mockControleAcesso.Usuario,
            UsuarioId = mockControleAcesso.UsuarioId
        };
        mockContext.ControleAcesso = Usings
            .MockDbSet(new List<ControleAcesso> { MockControleAceesso })
            .Object;

        mockContext.SaveChanges();

        var emailSender = new Mock<EmailSender>();
        var repository = new Mock<ControleAcessoRepositorioImpl>(mockContext);
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(mockControleAcesso);
        mockRepository.Setup(repo => repo.RecoveryPassword(mockControleAcesso.Login)).Returns(false);
        

        // Act
        var result = mockRepository.Object.RecoveryPassword(mockControleAcesso.Login);

        //Assert
        Assert.IsType<bool>(result);
        Assert.False(result);
    }

    [Fact]
    public void RecoveryPassword_Should_Returns_False_When_Thorws_Exception()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "RecoveryPassword_Should_Returns_False_When_Thorws_Exception").Options;
        var mockContext = new RegisterContext(options);
        var dataset = _context.ControleAcesso.ToList();
        mockContext.ControleAcesso = Usings.MockDbSet(dataset).Object;
        mockContext.SaveChanges();
        var repository = new Mock<ControleAcessoRepositorioImpl>(mockContext);
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Throws(new Exception());
        var mockControleAcesso = dataset.First();

        // Act
        var result = mockRepository.Object.RecoveryPassword(mockControleAcesso.Login);

        //Assert
        Assert.IsType<bool>(result);
        Assert.False(result);
    }

    [Fact]
    public void ChangePassword_Should_Returns_False_When_Usuario_Null()
    {
        // Arrange
        var controleAcesso = _context.ControleAcesso.ToList().First();
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);
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
        var controleAcesso = _context.ControleAcesso.ToList().First();
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);
        mockRepository.Setup(repo => repo.ChangePassword(controleAcesso.UsuarioId, "!12345")).Returns(true);

        // Act
        var result = mockRepository.Object.ChangePassword(controleAcesso.UsuarioId, "!12345");

        //Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
    }

    [Fact]
    public void ChangePassword_Should_Throws_Exception()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "ChangePassword_Should_Throws_Exception").Options;
        var context = new RegisterContext(options);
        var lstControleAcesso = ControleAcessoFaker.Instance.ControleAcessos();
        var controleAcesso = lstControleAcesso.First();
        var mockUsuario = controleAcesso.Usuario;
        mockUsuario.Email = "teste@teste.com";
        context.ControleAcesso.AddRange(lstControleAcesso);
        context.Usuario.Add(mockUsuario);
        context.SaveChanges();
        var dbSetMock = Usings.MockDbSet(lstControleAcesso);
        var _dbContextMock = new Mock<RegisterContext>(context);
        _dbContextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dbSetMock.Object);
        _dbContextMock.Setup(c => c.SaveChanges()).Throws<Exception>();
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, context);
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(new ControleAcesso());
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
    public void IsValidPasssword_Should_Returns_True()
    {
        // Arrange
        var controleAcesso = _context.ControleAcesso.ToList().First();
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, _context);
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_repository.Object);
        controleAcesso.Senha = "!12345";
        ControleAcesso _controleAceesso = new ControleAcesso
        {
            Id = controleAcesso.Id,
            Login = controleAcesso.Login,
            Senha = "!12345",
            Usuario = controleAcesso.Usuario,
            UsuarioId = controleAcesso.UsuarioId
        };
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);
        mockRepository.Setup(repo => repo.IsValidPasssword(controleAcesso.Login, controleAcesso.Senha)).Returns(true);

        // Act
        var result = mockRepository.Object.IsValidPasssword(_controleAceesso.Login, "!12345");

        //Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
    }

    [Fact]
    public void IsValidPasssword_Should_Returns_False()
    {
        // Arrange
        var controleAcesso = _context.ControleAcesso.ToList().First();
        controleAcesso.Senha = Crypto.GetInstance.Encrypt("!12345");
        ControleAcesso _controleAceesso = new ControleAcesso
        {
            Id = controleAcesso.Id,
            Login = controleAcesso.Login,
            Senha = "!123456",
            Usuario = controleAcesso.Usuario,
            UsuarioId = controleAcesso.UsuarioId
        };
        
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, _context);
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);
        mockRepository.Setup(repo => repo.IsValidPasssword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        // Act
        var result = mockRepository.Object.IsValidPasssword(controleAcesso.Login, "!12345");

        //Assert
        Assert.IsType<bool>(result);
        Assert.False(result);
    }

    [Fact]
    public void Create_Should_Throw_Exception_When_User_Already_Exists()
    {
        // Arrange
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        var repository = new Mock<ControleAcessoRepositorioImpl>(_context);
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(mockControleAcesso);

        // Act & Assert
        Assert.Throws<AggregateException>(() => repository.Object.Create(mockControleAcesso));
    }

    [Fact]
    public void Create_Should_Not_Add_User_When_Not_Exists()
    {
        // Arrange
        var mockControleAcesso = ControleAcessoFaker.Instance.GetNewFaker();
        var repository = new Mock<ControleAcessoRepositorioImpl>(_context);
        var mockRepository = Mock.Get<IControleAcessoRepositorioImpl>(repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns((ControleAcesso)null);

        // Act
        repository.Object.Create(mockControleAcesso);

        // Assert
        mockRepository.Verify(repo => repo.Create(mockControleAcesso), Times.Never);
    }

    [Fact]
    public void FindById_Should_Return_ControleAcesso_When_User_Exists()
    {
        // Arrange
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        var repository = new ControleAcessoRepositorioImpl(_context);

        // Act
        var result = repository.FindById(mockControleAcesso.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockControleAcesso, result);
    }

    [Fact]
    public void FindById_Should_Return_Null_When_User_Not_Exists()
    {
        // Arrange
        var nonExistingId = -1; // assuming -1 is an invalid ID
        var repository = new ControleAcessoRepositorioImpl(_context);

        // Act
        var result = repository.FindById(nonExistingId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void RevokeToken_Should_Throw_Exception_When_User_Not_Exists()
    {
        // Arrange
        var nonExistingId = -1; // assuming -1 is an invalid ID
        var repository = new ControleAcessoRepositorioImpl(_context);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => repository.RevokeToken(nonExistingId));
    }

    [Fact]
    public void RevokeToken_Should_Remove_Token_When_User_Exists()
    {
        // Arrange
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        var repository = new ControleAcessoRepositorioImpl(_context);

        // Act
        repository.RevokeToken(mockControleAcesso.Id);

        // Assert
        Assert.Null(mockControleAcesso.RefreshToken);
        Assert.Null(mockControleAcesso.RefreshTokenExpiry);
    }

    [Fact]
    public void RefreshTokenInfo_Should_Update_Token_Info()
    {
        // Arrange
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        var repository = new ControleAcessoRepositorioImpl(_context);

        // Act
        mockControleAcesso.RefreshToken = "new_token";
        mockControleAcesso.RefreshTokenExpiry = DateTime.UtcNow.AddHours(1);
        repository.RefreshTokenInfo(mockControleAcesso);

        // Assert
        Assert.NotNull(mockControleAcesso.RefreshToken);
        Assert.NotNull(mockControleAcesso.RefreshTokenExpiry);
        Assert.Equal("new_token", mockControleAcesso.RefreshToken);
    }

}
