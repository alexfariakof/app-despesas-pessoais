
using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories;
using despesas_backend_api_net_core.XUnit.Fakers;

namespace Test.XUnit.Business.Implementations
{
    public class LancamentoBusinessImplTest
    {
        private readonly Mock<ILancamentoRepositorio> _repositorioMock;
        private readonly LancamentoBusinessImpl _lancamentoBusiness;


        public LancamentoBusinessImplTest()
        {
            _repositorioMock = new Mock<ILancamentoRepositorio>();
            _lancamentoBusiness = new LancamentoBusinessImpl(_repositorioMock.Object);
        }

        [Fact]
        public void FindByMesAno_ShouldReturnListOfLancamentoVM()
        {
            // Mock the repository method to return some data
            var lancamentos = LancamentoFaker.Lancamentos();

            // Arrange            
            var data = lancamentos.First().Data;
            var idUsuario = lancamentos.First().UsuarioId;

            
            _repositorioMock.Setup(r => r.FindByMesAno(data, idUsuario)).Returns(lancamentos.FindAll(l => l.UsuarioId == idUsuario));

            // Act
            var result = _lancamentoBusiness.FindByMesAno(data, idUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<LancamentoVM>>(result);
            
            Assert.Equal(lancamentos.FindAll(l => l.UsuarioId == idUsuario).Count, result.Count);
        }

        [Fact]
        public void GetSaldo_ShouldReturnDecimal()
        {
            // Arrange
            var idUsuario = 1;

            // Mock the repository method to return a decimal value
            var saldo = 100.50m;
            _repositorioMock.Setup(r => r.GetSaldo(idUsuario)).Returns(saldo);

            // Act
            var result = _lancamentoBusiness.GetSaldo(idUsuario);

            // Assert
            Assert.Equal(saldo, result);
        }

        [Fact]
        public void GetDadosGraficoByAnoByIdUsuario_ShouldReturnGrafico()
        {
            // Arrange
            var idUsuario = 1;
            var data = new DateTime(2023, 10, 1);

            // Mock the repository method to return a Grafico object
            var graficoData = GraficoFaker.GetNewFaker();
            _repositorioMock.Setup(r => r.GetDadosGraficoByAno(idUsuario, data)).Returns(graficoData);

            // Act
            var result = _lancamentoBusiness.GetDadosGraficoByAnoByIdUsuario(idUsuario, data);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Grafico>(result);
        }
    }
}