using Business.Dtos.Parser;
using Domain.Core.Interfaces;

namespace Business;
public class ImagemPerfilUsuarioBusinessImplTests
{
    private readonly Mock<IRepositorio<ImagemPerfilUsuario>> _repositorioMock;
    private readonly Mock<IRepositorio<Usuario>> _repositorioUsuarioMock;
    private readonly ImagemPerfilUsuarioBusinessImpl _imagemPerfilUsuarioBusiness;
    private readonly Mock<IAmazonS3Bucket> _mockAmazonS3Bucket;
    private List<ImagemPerfilUsuario> _imagensPerfil;

    public ImagemPerfilUsuarioBusinessImplTests()
    {
        _imagensPerfil = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarios();
        
        _repositorioMock = Usings.MockRepositorio(_imagensPerfil);
        _repositorioUsuarioMock = new Mock<IRepositorio<Usuario>>(MockBehavior.Default);
        _mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>();
        _imagemPerfilUsuarioBusiness = new ImagemPerfilUsuarioBusinessImpl(_repositorioMock.Object, _repositorioUsuarioMock.Object, _mockAmazonS3Bucket.Object);
        _imagensPerfil = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarios();
    }

    [Fact]
    public void Create_Should_Returns_ImagemPerfilUsuarioDto()
    {
        // Arrange
        var imagemPerfilVM = ImagemPerfilUsuarioFaker.GetNewFakerVM(UsuarioFaker.Instance.GetNewFakerVM());
        var imagemPerfil = new ImagemPerfilUsuarioParser().Parse(imagemPerfilVM);
        _mockAmazonS3Bucket.Setup(x => x.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuario>(), It.IsAny<byte[]>())).ReturnsAsync("http://teste.url");
        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<ImagemPerfilUsuario>.IsAny));

        // Act
        var result = _imagemPerfilUsuarioBusiness.Create(imagemPerfilVM);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ImagemPerfilDto>(result);
    }

    [Fact]
    public void Create_Should_Throws_Exception_And_Return_Null()
    {
        // Arrange
        var imagemPerfilVM = ImagemPerfilUsuarioFaker.GetNewFakerVM(UsuarioFaker.Instance.GetNewFakerVM());
        var imagemPerfil = new ImagemPerfilUsuarioParser().Parse(imagemPerfilVM);
        _mockAmazonS3Bucket.Setup(x => x.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuario>(), It.IsAny<byte[]>())).Throws<Exception>();
        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<ImagemPerfilUsuario>.IsAny));

        // Act
        var result = _imagemPerfilUsuarioBusiness.Create(imagemPerfilVM);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void FindAll_Should_Return_List_Of_ImagemPerfilUsuarioDto()
    {
        // Arrange
        var imagemPerfilVM = new ImagemPerfilUsuarioParser().Parse(_imagensPerfil.First());
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil);

        // Act
        var result = _imagemPerfilUsuarioBusiness.FindAll(imagemPerfilVM.IdUsuario);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<ImagemPerfilDto>>(result);
    }

    [Fact]
    public void FindById_Should_Returns_ImagemPerfilUsuarioDto()
    {
        // Arrange
        var imagemPerfilVM = new ImagemPerfilUsuarioParser().Parse(_imagensPerfil.First());
        _repositorioMock.Setup(repo => repo.Get(imagemPerfilVM.Id)).Returns(_imagensPerfil.Find(obj => obj.Id == imagemPerfilVM.Id) ?? new());

        // Act
        var result = _imagemPerfilUsuarioBusiness.FindById(imagemPerfilVM.Id, imagemPerfilVM.IdUsuario);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ImagemPerfilDto>(result);
    }

    [Fact]
    public void FindById_Should_Returns_Null()
    {
        // Arrange
        var imagemPerfilVM = new ImagemPerfilUsuarioParser().Parse(_imagensPerfil.First());
        _repositorioMock.Setup(repo => repo.Get(imagemPerfilVM.Id)).Returns((ImagemPerfilUsuario)null);

        // Act
        var result = _imagemPerfilUsuarioBusiness.FindById(imagemPerfilVM.Id, imagemPerfilVM.IdUsuario);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void FindByIdUsuario_Should_Return_Usuario()
    {
        // Arrange
        var usuario = _imagensPerfil.First().Usuario;
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil);

        // Act
        var result = _imagemPerfilUsuarioBusiness.FindByIdUsuario(usuario.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UsuarioDto>(result);
    }

    [Fact]
    public void FindByIdUsuario_Throws_Exception_And_Returns_Null()
    {
        // Arrange
        var usuario = new Usuario { Id = 0 };
        _repositorioMock.Setup(repo => repo.GetAll()).Throws<Exception>(null);

        // Act
        var result = _imagemPerfilUsuarioBusiness.FindByIdUsuario(usuario.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Update_Should_Return_ImagemPerfilUsuarioDto()
    {
        // Arrange
        var imagemPerfil = _imagensPerfil.First();
        var imagemPerfilVM = new ImagemPerfilUsuarioParser().Parse(imagemPerfil);
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.IdUsuario));
        _repositorioMock.Setup(repo => repo.Update(ref It.Ref<ImagemPerfilUsuario>.IsAny));
        _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuario>())).ReturnsAsync(true);
        _mockAmazonS3Bucket.Setup(x => x.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuario>(), It.IsAny<byte[]>())).ReturnsAsync("http://teste.url");

        // Act
        var result = _imagemPerfilUsuarioBusiness.Update(imagemPerfilVM);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ImagemPerfilDto>(result);
    }

    [Fact]
    public void Update_Should_Return_Null()
    {
        // Arrange
        var imagemPerfil = _imagensPerfil.First();
        var imagemPerfilVM = new ImagemPerfilUsuarioParser().Parse(imagemPerfil);
        _repositorioMock.Setup(repo => repo.GetAll()).Throws<Exception>(null);

        // Act
        var result = _imagemPerfilUsuarioBusiness.Update(imagemPerfilVM);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Update_Should_Return_Null_When_Try_To_Delete()
    {
        // Arrange
        var imagemPerfil = _imagensPerfil.First();
        var imagemPerfilVM = new ImagemPerfilUsuarioParser().Parse(imagemPerfil);
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.IdUsuario));
        _repositorioMock.Setup(repo => repo.Update(ref It.Ref<ImagemPerfilUsuario>.IsAny));
        _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuario>())).Throws<Exception>();
        _mockAmazonS3Bucket.Setup(x => x.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuario>(), It.IsAny<byte[]>())).ReturnsAsync("http://teste.url");

        // Act
        var result = _imagemPerfilUsuarioBusiness.Update(imagemPerfilVM);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Delete_Should_Return_True()
    {
        // Arrange
        var imagemPerfilVM = new ImagemPerfilUsuarioParser().Parse(_imagensPerfil.First());
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.IdUsuario));
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<ImagemPerfilUsuario>())).Returns(true);
        _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuario>())).ReturnsAsync(true);

        // Act
        var result = _imagemPerfilUsuarioBusiness.Delete(imagemPerfilVM.IdUsuario);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_Should_Return_False_When_Try_To_Delete()
    {
        // Arrange
        var imagemPerfilVM = new ImagemPerfilUsuarioParser().Parse(_imagensPerfil.First());
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.IdUsuario));
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<ImagemPerfilUsuario>())).Returns(true);
        _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuario>())).ReturnsAsync(false);

        // Act
        var result = _imagemPerfilUsuarioBusiness.Delete(imagemPerfilVM.IdUsuario);

        // Assert
        Assert.False(result);
    }
}
