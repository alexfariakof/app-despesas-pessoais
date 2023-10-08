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
            Assert.Equal(despesaVM.Id, result.Id);
        }

        [Fact]
        public void FindAll_ReturnsListOfDespesaVM()
        {
            // Arrange         
            var despesas = Usings.lstDespesas;
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(despesas);

            // Act
            var result = _despesaBusiness.FindAll(1);

            // Assert
            Assert.Equal(despesas.Count, result.Count);
            // Assert other properties as needed
        }

        [Fact]
        public void FindById_ReturnsParsedDespesaVM()
        {
            // Arrange
            var id = 1;
            var despesa = Usings.lstDespesas.Find(d => d.Id == 2);

            _repositorioMock.Setup(repo => repo.Get(id)).Returns(despesa);

            // Act
            var result = _despesaBusiness.FindById(id, 1);

            // Assert
            Assert.Equal(despesa.Id, result.Id);
            // Assert other properties as needed
        }

        [Fact]
        public void FindById_ShouldReturnsNullWhenParsedDespesaVM()
        {
            // Arrange
            var id = 0;
            var despesa = Usings.lstDespesas.Find(d => d.Id == 2);

            _repositorioMock.Setup(repo => repo.Get(id)).Returns(despesa);

            // Act
            var result = _despesaBusiness.FindById(id, 0);

            // Assert
            Assert.Equal(result, null);
            // Assert other properties as needed
        }


        [Fact]
        public void Update_ReturnsParsedDespesaVM()
        {
            // Arrange
            // Arrange
            var despesaVM = new DespesaVM
            {
                Id = 2,
                Data = DateTime.Now.AddDays(new Random().Next(99)),
                Descricao = "Teste Despesas 2",
                Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(),
                DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),
                IdUsuario = 1,
                IdCategoria = 25
            };

            var despesa = new DespesaMap().Parse(despesaVM);

            _repositorioMock.Setup(repo => repo.Update(It.IsAny<Despesa>())).Returns(despesa);

            // Act
            var result = _despesaBusiness.Update(despesaVM);

            // Assert
            Assert.Equal(despesa.Id, result.Id);
            // Assert other properties as needed
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