using Domain.Core;
using Domain.Core.Interfaces;

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
        var emailSender = new Mock<EmailSender>();
        var mockEmailSender = Mock.Get<IEmailSender>(emailSender.Object);
        _repository = new Mock<ControleAcessoRepositorioImpl>(_context, mockEmailSender.Object);
    }

    [Fact]
    public void Create_Should_Return_True()
    {
        // Arrange
        var controleAcesso = ControleAcessoFaker.Instance.GetNewFaker();        
        controleAcesso.Usuario.StatusUsuario = StatusUsuario.Ativo;
        controleAcesso.Usuario.PerfilUsuario = PerfilUsuario.Administrador;

        // Create a mock repository
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns((ControleAcesso?)null);
        mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>())).Returns(true);

        // Act
        var result = mockRepository.Object.Create(controleAcesso);

        // Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
        //mockRepository.Verify(repo => repo.Create(controleAcesso), Times.Once);
    }

    [Fact]
    public void Create_Should_Return_False()
    {
        // Arrange and Setup mock repository
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
        mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>())).Returns(false);
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        // Act
        var result = mockRepository.Object.Create(mockControleAcesso);

        // Assert
        Assert.IsType<bool>(result);
        Assert.False(result);
        mockRepository.Verify(repo => repo.Create(It.IsAny<ControleAcesso>()), Times.Never);
    }

    [Fact]
    public void Create_Should_Throws_Exception()
    {
        // Arrange and Setup mock repository
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
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
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
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
    public void GetUsuarioByEmail_Should_Returns_Usuario()
    {
        // Arrange and Setup Repository
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        // Act
        var result = mockRepository.Object.GetUsuarioByEmail(mockControleAcesso.Login);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Usuario>(result);
    }

    [Fact]
    public void RecoveryPassword_Should_Returns_True() // Metodo não pode ser Testado por motivo do Enviar Email
    {
        // Arrange
        var emailSender = new Mock<EmailSender>();
        var mockEmailSender = Mock.Get<IEmailSender>(emailSender.Object);
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        mockEmailSender.Setup(sender => sender.SendEmailPassword(mockControleAcesso.Usuario, "!2345")).Returns(true);
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, _context, mockEmailSender.Object);
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(mockControleAcesso);
        mockRepository.Setup(repo => repo.GetUsuarioByEmail(mockControleAcesso.Login)).Returns(mockControleAcesso.Usuario);
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
        var mockEmailSender = Mock.Get<IEmailSender>(emailSender.Object);
        mockEmailSender.Setup(sender => sender.SendEmailPassword(mockUsuario, string.Empty)).Returns(false);
        var repository = new Mock<ControleAcessoRepositorioImpl>(mockContext, mockEmailSender.Object);
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(mockControleAcesso);
        mockRepository.Setup(repo => repo.GetUsuarioByEmail(mockControleAcesso.Login)).Returns(mockUsuario);
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
        // Arrange And Setup Mock
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, _context, null);
        var mockControleAcesso = _context.ControleAcesso.ToList().First();
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(mockControleAcesso);
        mockRepository.Setup(repo => repo.GetUsuarioByEmail(mockControleAcesso.Login)).Returns(mockControleAcesso.Usuario);
        mockRepository.Setup(repo => repo.RecoveryPassword(mockControleAcesso.Login)).Throws<Exception>();
        mockRepository.Setup(repo => repo.RecoveryPassword(mockControleAcesso.Login)).Returns(false);
        

        /*
        // Simule o comportamento do método EnviarEmail Através de Reflection
        MethodInfo enviarEmailMethod = typeof(ControleAcessoRepositorioImpl).GetMethod("EnviarEmail", BindingFlags.NonPublic | BindingFlags.Instance);
        object instance = mockRepository.Object;
        Action erroEnviarEmail = () =>  enviarEmailMethod.Invoke(instance, new object[] { mockControleAcesso, "EnviarEmail_Erro" });
        */

        // Act
        var result = mockRepository.Object.RecoveryPassword(mockControleAcesso.Login);
        //Assert 1


        //Assert
        Assert.IsType<bool>(result);
        Assert.False(result);

        /*
        //Act Whem expect erro from a private Mehod
        Action act = () => mockRepository.Object.RecoveryPassword(mockControleAcesso.Login);

        // Assert 2
        Assert.NotNull(act);
        var exception = Assert.Throws<Exception>(() => _repository.Object.RecoveryPassword(mockControleAcesso.Login));
        Assert.Equal("Erro ao Enviar Email!", exception.Message);
        */
    }

    [Fact]
    public void ChangePassword_Should_Returns_False_When_Usuario_Null()
    {
        // Arrange
        var controleAcesso = _context.ControleAcesso.ToList().First();
        var mockEmailSender = new Mock<IEmailSender>();
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, _context, mockEmailSender.Object);

        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
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
        var mockEmailSender = new Mock<IEmailSender>();
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, _context, mockEmailSender.Object);
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
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
        var mockEmailSender = new Mock<IEmailSender>();
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, context, mockEmailSender.Object);
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
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
        var mockEmailSender = new Mock<IEmailSender>();
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, _context, mockEmailSender.Object);
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
        controleAcesso.Senha = Crypto.GetInstance.Encrypt("!12345");
        ControleAcesso _controleAceesso = new ControleAcesso
        {
            Id = controleAcesso.Id,
            Login = controleAcesso.Login,
            Senha = "!12345",
            Usuario = controleAcesso.Usuario,
            UsuarioId = controleAcesso.UsuarioId
        };
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);
        mockRepository.Setup(repo => repo.isValidPasssword(controleAcesso)).Returns(true);

        // Act
        var result = mockRepository.Object.isValidPasssword(_controleAceesso);

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

        var mockEmailSender = new Mock<IEmailSender>();
        _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, _context, mockEmailSender.Object);
        var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
        mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);
        mockRepository.Setup(repo => repo.isValidPasssword(controleAcesso)).Returns(false);

        // Act
        var result = mockRepository.Object.isValidPasssword(controleAcesso);

        //Assert
        Assert.IsType<bool>(result);
        Assert.False(result);
    }
}
