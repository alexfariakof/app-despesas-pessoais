using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace Test.XUnit.Business.Implementations
{
    public class ReceitaBusinessImplTest
    {
        private readonly Mock<IRepositorio<Receita>> _repositorioMock;
        private readonly ReceitaBusinessImpl _receitaBusiness;
        private List<Receita> _receitaList;
        private List<ReceitaVM> _receitaListVM;

        public ReceitaBusinessImplTest()
        {
            _repositorioMock = new Mock<IRepositorio<Receita>>();
            _receitaBusiness = new ReceitaBusinessImpl(_repositorioMock.Object);
            _receitaList = ReceitaFaker.Receitas();
            _receitaListVM = ReceitaFaker.ReceitasVMs();
        }

        [Fact]
        public void Create_ReturnsParsedReceitaVM()
        {
            // Arrange
            var receitaVM = _receitaListVM.First();

            _repositorioMock.Setup(repo => repo.Insert(It.IsAny<Receita>())).Returns(new ReceitaMap().Parse(receitaVM));

            // Act
            var result = _receitaBusiness.Create(receitaVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ReceitaVM>(result);
            Assert.Equal(receitaVM.Id, result.Id);
        }

        [Fact]
        public void FindAll_ReturnsListOfReceitaVM()
        {
            // Arrange         
            var idUsuario = _receitaList.First().Id;
            var lstReceitas = _receitaList.FindAll(r => r.UsuarioId.Equals(idUsuario));

            _repositorioMock.Setup(repo => repo.GetAll()).Returns(lstReceitas);

            // Act
            var result = _receitaBusiness.FindAll(idUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ReceitaVM>>(result);
            Assert.Equal(lstReceitas.Count, result.Count);            
        }

        [Fact]
        public void FindById_ReturnsParsedReceitaVM()
        {
            // Arrange
            var id = _receitaList.First().Id;
            var receita = _receitaList.First();

            _repositorioMock.Setup(repo => repo.Get(id)).Returns(receita);

            // Act
            var result = _receitaBusiness.FindById(id, receita.UsuarioId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ReceitaVM>(result);
            Assert.Equal(receita.Id, result.Id);
        }

        [Fact]
        public void FindById_ShouldReturnsNullWhenParsedReceitaVM()
        {
            // Arrange
            var id = 0;
            var receita = _receitaList[0];

            _repositorioMock.Setup(repo => repo.Get(id)).Returns(receita);

            // Act
            var result = _receitaBusiness.FindById(id, 0);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public void Update_ReturnsParsedReceitaVM()
        {
            // Arrange
            var receitaVM = _receitaListVM.First();
            var receita = new ReceitaMap().Parse(receitaVM);            

            _repositorioMock.Setup(repo => repo.Update(It.IsAny<Receita>())).Returns(receita);

            // Act
            var result = _receitaBusiness.Update(receitaVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ReceitaVM>(result);
            Assert.Equal(receita.Id, result.Id);
        }

        [Fact]
        public void Delete_ReturnsTrue()
        {
            // Arrange
            var receita = _receitaList.First();
            _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Receita>())).Returns(true);

            var receitaVM = new ReceitaMap().Parse(receita);
            // Act
            var result = _receitaBusiness.Delete(receitaVM);

            // Assert
            Assert.True(result);
        }
    }
}