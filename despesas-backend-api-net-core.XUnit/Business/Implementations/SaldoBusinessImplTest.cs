
using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories;

namespace Test.XUnit.Business.Implementations
{
    public class SaldoBusinessImplTest
    {
        private readonly Mock<ISaldoRepositorio> _repositorioMock;
        private readonly SaldoBusinessImpl _saldoBusiness;

        public SaldoBusinessImplTest()
        {
            _repositorioMock = new Mock<ISaldoRepositorio>();
            _saldoBusiness = new SaldoBusinessImpl(_repositorioMock.Object);
        }

        [Fact]
        public void GetSaldo_Should_Return_Saldo_As_Decimal()
        {
            // Arrange
            var idUsuario = 1;
            var saldo = 100.50m;
            _repositorioMock.Setup(r => r.GetSaldo(idUsuario)).Returns(saldo);

            // Act
            var result = _saldoBusiness.GetSaldo(idUsuario);

            // Assert
            Assert.Equal(saldo, result);
            _repositorioMock.Verify(r => r.GetSaldo(idUsuario), Times.Once);
        }

        [Fact]
        public void GetSaldoByAno_Should_Return_Saldo_As_Decimal()
        {
            // Arrange
            var idUsuario = 1;
            var saldo = 300.33m;
            _repositorioMock.Setup(r => r.GetSaldoByAno(DateTime.Today, idUsuario)).Returns(saldo);

            // Act
            var result = _saldoBusiness.GetSaldoAnual(DateTime.Today,  idUsuario);

            // Assert
            Assert.Equal(saldo, result);
            _repositorioMock.Verify(r => r.GetSaldoByAno(DateTime.Today, idUsuario), Times.Once);
        }

        [Fact]
        public void GetSaldoByMesAno_Should_Return_Saldo_As_Decimal()
        {
            // Arrange
            var idUsuario = 1;
            var saldo = 222.22m;
            _repositorioMock.Setup(r => r.GetSaldoByMesAno(DateTime.Today, idUsuario)).Returns(saldo);

            // Act
            var result = _saldoBusiness.GetSaldoByMesAno(DateTime.Today, idUsuario);

            // Assert
            Assert.Equal(saldo, result);
            _repositorioMock.Verify(r => r.GetSaldoByMesAno(DateTime.Today, idUsuario), Times.Once);
        }
    }
}