using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;

namespace Test.XUnit.Business.Implementations
{
    public class DespesaBusinessImplTest
    {
        private readonly Mock<IRepositorio<Despesa>> _repositorioMock;
        private readonly DespesaBusinessImpl _despesaBusiness;

        public DespesaBusinessImplTest()
        {
            _repositorioMock = new Mock<IRepositorio<Despesa>>();
            _despesaBusiness = new DespesaBusinessImpl(_repositorioMock.Object);
        }

        [Fact]
        public void Create_ReturnsParsedDespesaVM()
        {
            // Arrange
            var despesaVM = new DespesaVM
            {
                Id = 2,
                Data = DateTime.Now.AddDays(new Random().Next(200)),
                Descricao = "Teste Despesas 2",
                Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(),
                DataVencimento = DateTime.Now.AddDays(new Random().Next(200)),
                IdUsuario = 1,
                IdCategoria = 25
            };

            _repositorioMock.Setup(repo => repo.Insert(It.IsAny<Despesa>())).Returns(new DespesaMap().Parse(despesaVM));

            // Act
            var result = _despesaBusiness.Create(despesaVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DespesaVM>(result);
            Assert.Equal(despesaVM.Id, result.Id);
        }

        [Fact]
        public void FindAll_ReturnsListOfDespesaVM()
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
        }

        [Fact]
        public void FindById_ReturnsParsedDespesaVM()
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
        }

        [Fact]
        public void FindById_ShouldReturnsNullWhenParsedDespesaVM()
        {
            // Arrange
            var despesa = DespesaFaker.Despesas().First();
            var id = despesa.Id;

            _repositorioMock.Setup(repo => repo.Get(id)).Returns(despesa);

            // Act
            var result = _despesaBusiness.FindById(id, 0);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Update_ReturnsParsedDespesaVM()
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
        }

        [Fact]
        public void Delete_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            _repositorioMock.Setup(repo => repo.Delete(id)).Returns(true);

            // Act
            var result = _despesaBusiness.Delete(id);

            // Assert
            Assert.True(result);
        }
    }
}