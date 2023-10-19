using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using despesas_backend_api_net_core.Infrastructure.Security;
using Microsoft.Extensions.Configuration;

namespace despesas_backend_api_net_core.XUnit.Business.Implementations
{
    public class ImagemPerfilUsuarioBusinessImplTests
    {
        private readonly Mock<IRepositorio<ImagemPerfilUsuario>> _repositorioMock;
        private readonly ImagemPerfilUsuarioBusinessImpl _imagemPerfilUsuarioBusiness;
        private readonly Mock<IAmazonS3Bucket> _mockAmazonS3Bucket;
        private List<ImagemPerfilUsuario> _imagensPerfil;

        public ImagemPerfilUsuarioBusinessImplTests()
        {
            _imagensPerfil = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarios();
            _repositorioMock = Usings.MockRepositorio(_imagensPerfil);
            _mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            
            var accessKey = configuration.GetSection("AmazonS3Bucket:accessKey").Value;
            var secretAccessKey = configuration.GetSection("AmazonS3Bucket:secretAccessKey").Value;
            var s3ServiceUrl = configuration.GetSection("AmazonS3Bucket:s3ServiceUrl").Value;
            var bucketName = configuration.GetSection("AmazonS3Bucket:bucketName").Value;
            var RegionEndpoint = configuration.GetSection("AmazonS3Bucket:RegionEndpoint").Value;
            _mockAmazonS3Bucket.Object.SetConfiguration(accessKey, secretAccessKey, s3ServiceUrl, bucketName);
                       

            _imagemPerfilUsuarioBusiness = new ImagemPerfilUsuarioBusinessImpl(_repositorioMock.Object, _mockAmazonS3Bucket.Object);
            _imagensPerfil = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarios();
        }

        [Fact]
        public void Create_Should_Returns_ImagemPerfilUsuarioVM()
        {
            // Arrange
            var imagemPerfilVM = ImagemPerfilUsuarioFaker.GetNewFakerVM(UsuarioFaker.GetNewFakerVM());
            var imagemPerfil = new ImagemPerfilUsuarioMap().Parse(imagemPerfilVM);

            _mockAmazonS3Bucket.Setup(a => a.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuarioVM>())).ReturnsAsync("http://teste.url");
            _repositorioMock.Setup(repo => repo.Insert(It.IsAny<ImagemPerfilUsuario>())).Returns(imagemPerfil);

            // Act
            var result = _imagemPerfilUsuarioBusiness.Create(imagemPerfilVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ImagemPerfilUsuarioVM>(result);
        }

        [Fact]
        public void Create_Should_Throws_Exception_And_Return_Null()
        {
            // Arrange
            var imagemPerfilVM = ImagemPerfilUsuarioFaker.GetNewFakerVM(UsuarioFaker.GetNewFakerVM());
            var imagemPerfil = new ImagemPerfilUsuarioMap().Parse(imagemPerfilVM);

            _mockAmazonS3Bucket.Setup(a => a.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuarioVM>())).Throws<Exception>();
            _repositorioMock.Setup(repo => repo.Insert(It.IsAny<ImagemPerfilUsuario>())).Returns(imagemPerfil);

            // Act
            var result = _imagemPerfilUsuarioBusiness.Create(imagemPerfilVM);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void FindAll_Should_Return_List_Of_ImagemPerfilUsuarioVM()
        {
            // Arrange
            var imagemPerfilVM = new ImagemPerfilUsuarioMap().Parse(_imagensPerfil.First());
            
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil);

            // Act
            var result = _imagemPerfilUsuarioBusiness.FindAll(imagemPerfilVM.IdUsuario); 

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ImagemPerfilUsuarioVM>>(result);
        }

        [Fact]
        public void FindById_Should_Returns_ImagemPerfilUsuarioVM()
        {
            // Arrange
            var imagemPerfilVM = new ImagemPerfilUsuarioMap().Parse(_imagensPerfil.First());

            _repositorioMock.Setup(repo => repo.Get(imagemPerfilVM.Id)).Returns(_imagensPerfil.Find(obj => obj.Id == imagemPerfilVM.Id));

            // Act
            var result = _imagemPerfilUsuarioBusiness.FindById(imagemPerfilVM.Id, imagemPerfilVM.IdUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ImagemPerfilUsuarioVM>(result);
        }

        [Fact]
        public void FindById_Should_Returns_Null()
        {
            // Arrange
            var imagemPerfilVM = new ImagemPerfilUsuarioMap().Parse(_imagensPerfil.First());

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
            Assert.IsType<UsuarioVM>(result);
        }

        [Fact]
        public void FindByIdUsuario_Throws_Exception_And_Returns_Null()
        {
            // Arrange
            var usuario = new Usuario { Id = 0 };

            _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil);

            // Act
            var result = _imagemPerfilUsuarioBusiness.FindByIdUsuario(usuario.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Update_Should_Return_ImagemPerfilUsuarioVM()
        {
            // Arrange
            var imagemPerfil = _imagensPerfil.First();
            var imagemPerfilVM = new ImagemPerfilUsuarioMap().Parse(imagemPerfil);

            _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.IdUsuario));
            _repositorioMock.Setup(repo => repo.Update(It.IsAny<ImagemPerfilUsuario>())).Returns(imagemPerfil);
            _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuarioVM>())).ReturnsAsync(true);
            _mockAmazonS3Bucket.Setup(a => a.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuarioVM>())).ReturnsAsync("http://teste.url");

            // Act
            var result = _imagemPerfilUsuarioBusiness.Update(imagemPerfilVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ImagemPerfilUsuarioVM>(result);
        }

        [Fact]
        public void Update_Should_Return_Null()
        {
            // Arrange
            var imagemPerfil = _imagensPerfil.First();
            var imagemPerfilVM = new ImagemPerfilUsuarioMap().Parse(imagemPerfil);

            _repositorioMock.Setup(repo => repo.GetAll()).Returns((List<ImagemPerfilUsuario>)null);
            _repositorioMock.Setup(repo => repo.Update(It.IsAny<ImagemPerfilUsuario>())).Returns(imagemPerfil);
            _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuarioVM>())).ReturnsAsync(true);
            _mockAmazonS3Bucket.Setup(a => a.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuarioVM>())).ReturnsAsync("http://teste.url");

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
            var imagemPerfilVM = new ImagemPerfilUsuarioMap().Parse(imagemPerfil);

            _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.IdUsuario));
            _repositorioMock.Setup(repo => repo.Update(It.IsAny<ImagemPerfilUsuario>())).Returns(imagemPerfil);
            _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuarioVM>())).ReturnsAsync(false);
            _mockAmazonS3Bucket.Setup(a => a.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuarioVM>())).ReturnsAsync("http://teste.url");

            // Act
            var result = _imagemPerfilUsuarioBusiness.Update(imagemPerfilVM);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Delete_Should_Return_True()
        {
            // Arrange
            var imagemPerfilVM = new ImagemPerfilUsuarioMap().Parse(_imagensPerfil.First());
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.IdUsuario));
            _repositorioMock.Setup(repo => repo.Delete(It.IsAny<ImagemPerfilUsuario>())).Returns(true);
            _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuarioVM>())).ReturnsAsync(true);

            // Act
            var result = _imagemPerfilUsuarioBusiness.Delete(imagemPerfilVM);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_Should_Return_False_When_Try_To_Delete()
        {
            // Arrange
            var imagemPerfilVM = new ImagemPerfilUsuarioMap().Parse(_imagensPerfil.First());
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(_imagensPerfil.FindAll(obj => obj.Usuario.Id == imagemPerfilVM.IdUsuario));
            _repositorioMock.Setup(repo => repo.Delete(It.IsAny<ImagemPerfilUsuario>())).Returns(true);
            _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuarioVM>())).ReturnsAsync(false);

            // Act
            var result = _imagemPerfilUsuarioBusiness.Delete(imagemPerfilVM);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Delete_Should_Return_Null()
        {
            // Arrange
            var imagemPerfilVM = new ImagemPerfilUsuarioMap().Parse(_imagensPerfil.First());
            _repositorioMock.Setup(repo => repo.GetAll()).Returns((List<ImagemPerfilUsuario>)null);
            _repositorioMock.Setup(repo => repo.Delete(It.IsAny<ImagemPerfilUsuario>())).Returns(true);
            _mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(It.IsAny<ImagemPerfilUsuarioVM>())).ReturnsAsync(true);

            // Act
            var result = _imagemPerfilUsuarioBusiness.Delete(imagemPerfilVM);

            // Assert
            Assert.False(result);
        }
    }
}