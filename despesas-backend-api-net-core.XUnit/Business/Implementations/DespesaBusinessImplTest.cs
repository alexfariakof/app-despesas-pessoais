using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace Test.XUnit.Business.Implementations
{
    public class DespesaBusinessImplTest
    {
        private readonly Mock<IRepositorio<Despesa>> _repositorioMock;
        private readonly Mock<IRepositorio<Categoria>> _repositorioCategoria;
        private readonly DespesaBusinessImpl _despesaBusiness;

        public DespesaBusinessImplTest()
        {
            _repositorioMock = new Mock<IRepositorio<Despesa>>();
            _repositorioCategoria = new Mock<IRepositorio<Categoria>>();
            _despesaBusiness = new DespesaBusinessImpl(_repositorioMock.Object, _repositorioCategoria.Object);
        }

        [Fact]
        public void Create_Should_Returns_Parsed_Despesa_VM()
        {
            // Arrange
            var despesaVM = DespesaFaker.DespesasVMs().First();

            _repositorioMock.Setup(repo => repo.Insert(It.IsAny<Despesa>())).Returns(new DespesaMap().Parse(despesaVM));

            // Act
            var result = _despesaBusiness.Create(despesaVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DespesaVM>(result);
            Assert.Equal(despesaVM.Id, result.Id);
            _repositorioMock.Verify(repo => repo.Insert(It.IsAny<Despesa>()), Times.Once);
        }

        [Fact]
        public void FindAll_Should_Returns_List_Of_DespesaVM()
        {
            // Arrange                     
            var despesas = DespesaFaker.Despesas();
            var despesa = despesas.First();
            var idUsuario = despesa.UsuarioId;
            despesas = despesas.FindAll(d => d.UsuarioId == idUsuario);
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(despesas);

            // Act
            var result = _despesaBusiness.FindAll(idUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<DespesaVM>>(result);
            Assert.Equal(despesas.Count, result.Count);
            _repositorioMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void FindById_Should_Returns_Parsed_DespesaVM()
        {
            // Arrange
            var despesa = DespesaFaker.Despesas().First();
            var id = despesa.Id;

            _repositorioMock.Setup(repo => repo.Get(id)).Returns(despesa);

            // Act
            var result = _despesaBusiness.FindById(id, despesa.UsuarioId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DespesaVM>(result);
            Assert.Equal(despesa.Id, result.Id);
            _repositorioMock.Verify(repo => repo.Get(id), Times.Once);
        }

        [Fact]
        public void FindById_Should_Returns_Null_When_Parsed_DespesaVM()
        {
            // Arrange
            var despesa = DespesaFaker.Despesas().First();
            var id = despesa.Id;

            _repositorioMock.Setup(repo => repo.Get(id)).Returns(despesa);

            // Act
            var result = _despesaBusiness.FindById(id, 0);

            // Assert
            Assert.Null(result);
            _repositorioMock.Verify(repo => repo.Get(id), Times.Once);
        }

        [Fact]
        public void Update_Should_Returns_Parsed_DespesaVM()
        {
            // Arrange         
            var despesaVM = DespesaFaker.DespesasVMs().First();
            var despesa = new DespesaMap().Parse(despesaVM);
            despesa.Descricao = "Teste Update Despesa";

            _repositorioMock.Setup(repo => repo.Update(It.IsAny<Despesa>())).Returns(despesa);

            // Act
            var result = _despesaBusiness.Update(despesaVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DespesaVM>(result);
            Assert.Equal(despesa.Id, result.Id);
            Assert.Equal(despesa.Descricao, result.Descricao);
            _repositorioMock.Verify(repo => repo.Update(It.IsAny<Despesa>()), Times.Once);
        }

        [Fact]
        public void Delete_Should_Returns_True()
        {
            // Arrange
            var despesa = DespesaFaker.Despesas().First();
            _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Despesa>())).Returns(true);
            var despesaVM = new DespesaMap().Parse(despesa);
            
            // Act
            var result = _despesaBusiness.Delete(despesaVM);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
            _repositorioMock.Verify(repo => repo.Delete(It.IsAny<Despesa>()), Times.Once);
        }
    }
}