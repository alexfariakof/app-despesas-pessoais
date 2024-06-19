using AutoMapper;
using Business.Dtos.Core.Profile;
using Business.Dtos.v2;
using Despesas.Infrastructure.Amazon.Abstractions;
using __mock__.v2;
using Repository.Persistency.Generic;
using Business.Implementations;

namespace Business;
public class ImagemPerfilUsuarioBusinessImplTests
{
    private readonly Mock<IRepositorio<ImagemPerfilUsuario>> _repositorioMock;
    private readonly Mock<IRepositorio<Usuario>> _repositorioUsuarioMock;
    private readonly ImagemPerfilUsuarioBusinessImpl<ImagemPerfilDto, UsuarioDto> _imagemPerfilUsuarioBusiness;
    private readonly Mock<IAmazonS3Bucket> _mockAmazonS3Bucket;
    private List<ImagemPerfilUsuario> _imagensPerfil;
    private Mapper _mapper;

    public ImagemPerfilUsuarioBusinessImplTests()
    {
        _imagensPerfil = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarios();
        
        _repositorioMock = Usings.MockRepositorio(_imagensPerfil);
        _repositorioUsuarioMock = new Mock<IRepositorio<Usuario>>(MockBehavior.Default);
        _mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>();
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<ImagemPerfilUsuarioProfile>(); }));
        _imagemPerfilUsuarioBusiness = new ImagemPerfilUsuarioBusinessImpl<ImagemPerfilDto, UsuarioDto>(_mapper, _repositorioMock.Object, _repositorioUsuarioMock.Object, _mockAmazonS3Bucket.Object);
        _imagensPerfil = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarios();
    }

    [Fact]
    public void Create_Should_Returns_ImagemPerfilUsuarioDto()
    {
        // Arrange
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var imagemPerfil = ImagemPerfilUsuarioFaker.Instance.GetNewFaker(usuario);
        var imagemPerfilDto = ImagemPerfilUsuarioFaker.Instance.GetNewDtoFrom(imagemPerfil);
        _mockAmazonS3Bucket.Setup(x => x.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuario>(), It.IsAny<byte[]>())).ReturnsAsync("http://teste.url");
        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<ImagemPerfilUsuario>.IsAny));
        _repositorioUsuarioMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(usuario);

        // Act
        var result = _imagemPerfilUsuarioBusiness.Create(imagemPerfilDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ImagemPerfilDto>(result);
    }

    [Fact]
    public void Create_Should_Throws_Exception_And_Return_Null()
    {
        // Arrange
        var imagemPerfilVM = ImagemPerfilUsuarioFaker.Instance.GetNewFakerDto(UsuarioFaker.Instance.GetNewFakerVM());
        var imagemPerfil = _mapper.Map<ImagemPerfilDto>(imagemPerfilVM);
        _mockAmazonS3Bucket.Setup(x => x.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuario>(), It.IsAny<byte[]>())).Throws<Exception>();
        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<ImagemPerfilUsuario>.IsAny));

        // Act & Assert 
        Assert.Throws<ArgumentException>(() => _imagemPerfilUsuarioBusiness.Create(imagemPerfilVM));
    }

    [Fact]
    public void FindAll_Should_Return_List_Of_ImagemPerfilUsuarioDto()
    {
        // Arrange
        var imagemPerfilVM = _mapper.Map<ImagemPerfilDto>(_imagensPerfil.First());
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil);

        // Act
        var result = _imagemPerfilUsuarioBusiness.FindAll(imagemPerfilVM.UsuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<ImagemPerfilDto>>(result);
    }

    [Fact]
    public void FindById_Should_Returns_ImagemPerfilUsuarioDto()
    {
        // Arrange
        var imagemPerfilVM = _mapper.Map<ImagemPerfilDto>(_imagensPerfil.First());
        _repositorioMock.Setup(repo => repo.Get(imagemPerfilVM.Id)).Returns(_imagensPerfil.Find(obj => obj.Id == imagemPerfilVM.Id) ?? new());

        // Act
        var result = _imagemPerfilUsuarioBusiness.FindById(imagemPerfilVM.Id, imagemPerfilVM.UsuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ImagemPerfilDto>(result);
    }

    [Fact]
    public void FindById_Should_Returns_Null()
    {
        // Arrange
        var imagem = _imagensPerfil.First();
        var imagemPerfilVM = _mapper.Map<ImagemPerfilDto>(imagem);
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(imagem);

        // Act
        var result = _imagemPerfilUsuarioBusiness.FindById(imagemPerfilVM.Id, 0);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void FindByIdUsuario_Should_Return_Usuario()
    {
        // Arrange
        var imagem = _imagensPerfil.Last();
        var usuario = imagem.Usuario;
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
        _repositorioMock.Setup(repo => repo.GetAll()).Throws<Exception>(() => null);

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
        var imagemPerfilVM = _mapper.Map<ImagemPerfilDto>(imagemPerfil);
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.UsuarioId));
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
    public void Update_Should_Throws_ArgumentException_when_ImagemPerfil_is_Null()
    {
        // Arrange
        var imagemPerfil = _imagensPerfil.First();
        var imagemPerfilVM = _mapper.Map<ImagemPerfilDto>(imagemPerfil);
        _repositorioMock.Setup(repo => repo.GetAll()).Throws<Exception>(() =>null);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _imagemPerfilUsuarioBusiness.Update(imagemPerfilVM));
    }

    [Fact]
    public void Update_Should_Return_Null_When_Try_To_Delete()
    {
        // Arrange
        var imagemPerfil = _imagensPerfil.First();
        var imagemPerfilVM = _mapper.Map<ImagemPerfilDto>(imagemPerfil);
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.UsuarioId));
        _repositorioMock.Setup(repo => repo.Update(ref It.Ref<ImagemPerfilUsuario>.IsAny));
        _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuario>())).Throws<Exception>();
        _mockAmazonS3Bucket.Setup(x => x.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuario>(), It.IsAny<byte[]>())).ReturnsAsync("http://teste.url");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _imagemPerfilUsuarioBusiness.Update(imagemPerfilVM));
    }

    [Fact]
    public void Delete_Should_Return_True()
    {
        // Arrange
        var imagemPerfilVM = _mapper.Map<ImagemPerfilDto>(_imagensPerfil.First());
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.UsuarioId));
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<ImagemPerfilUsuario>())).Returns(true);
        _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuario>())).ReturnsAsync(true);

        // Act
        var result = _imagemPerfilUsuarioBusiness.Delete(imagemPerfilVM.UsuarioId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_Should_Return_False_When_Try_To_Delete()
    {
        // Arrange
        var imagemPerfilVM = _mapper.Map<ImagemPerfilDto>(_imagensPerfil.First());
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.UsuarioId));
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<ImagemPerfilUsuario>())).Returns(true);
        _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuario>())).ReturnsAsync(false);

        // Act
        var result = _imagemPerfilUsuarioBusiness.Delete(imagemPerfilVM.UsuarioId);

        // Assert
        Assert.False(result);
    }
}
