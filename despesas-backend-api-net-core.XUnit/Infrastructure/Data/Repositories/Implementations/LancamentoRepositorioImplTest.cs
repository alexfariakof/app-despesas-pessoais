using despesas_backend_api_net_core.Infrastructure.Data.Repositories;
using Xunit.Extensions.Ordering;

namespace Infrastructure.Repositories
{
    [Order(213)]
    public class LancamentoRepositorioImplTest
    {
        private readonly RegisterContext _context;
        private Mock<ILancamentoRepositorio> _mockRepository;
        private Mock<LancamentoRepositorioImpl> _repository;
        private Usuario _mockUsuario;
        private DateTime _mockAnoMes;
        public LancamentoRepositorioImplTest()
        {
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "Lancamento Repo Database InMemory")
                .Options;
            _context = new RegisterContext(options);

            _mockUsuario = UsuarioFaker.GetNewFaker();
            _context.Usuario.Add(_mockUsuario);
            var despesas = DespesaFaker.Despesas(_mockUsuario, _mockUsuario.Id);
            var receitas = ReceitaFaker.Receitas(_mockUsuario, _mockUsuario.Id);
            _mockAnoMes = despesas.First().Data;
            _context.Despesa.AddRange(despesas);
            _context.Receita.AddRange(receitas);
            _context.SaveChanges();
            _repository = new Mock<LancamentoRepositorioImpl>(MockBehavior.Strict, _context);
            _mockRepository = Mock.Get<ILancamentoRepositorio>(_repository.Object);
        }

        [Fact, Order(1)]
        public void FindByMesAno_Should_Returns_List_lancamentos()
        {
            // Arrange
            var data = _mockAnoMes;
            var idUsuario = _mockUsuario.Id;

            // Act
            var result = _mockRepository.Object.FindByMesAno(data, idUsuario);

            // Assert            
            Assert.NotNull(result);
            Assert.IsType<List<Lancamento>>(result);
            Assert.True(result.Count >= 1);
        }

        [Fact, Order(2)]
        public void FindByMesAno_Should_Returns_Null_List_lancamentos()
        {
            // Arrange
            var data = _mockAnoMes;
            var idUsuario = _mockUsuario.Id;


            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "MemoryDatabase Lancamento ")
                .Options;

            var context = new RegisterContext(options);
            context.Despesa.AddRange(new List<Despesa>());
            context.Receita.AddRange(new List<Receita>());
            context.SaveChanges();
            var repository = new LancamentoRepositorioImpl(context);

            // Act
            var result = repository.FindByMesAno(data, idUsuario);

            // Assert            
            Assert.NotNull(result);
            Assert.IsType<List<Lancamento>>(result);
            Assert.True(result.Count == 0);
        }

        [Fact, Order(3)]
        public void FindByMesAno_Throws_Exception_When_Despesa_Execute_Where()
        {
            // Arrange
            var data = _mockAnoMes;
            var idUsuario = 0;

            var despesaDbSetMock = new Mock<DbSet<Despesa>>();
            despesaDbSetMock.As<IQueryable<Despesa>>()
               .Setup(d => d.Provider)
               .Throws<Exception>();

            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "FindByMesAno_Throws_Exception_When_Despesa_Execute_Where")
                .Options;

            var context = new RegisterContext(options);
            context.Despesa = despesaDbSetMock.Object;


            var repository = new LancamentoRepositorioImpl(context);

            // Act
            Action result = () => repository.FindByMesAno(data, idUsuario);

            // Assert
            Assert.NotNull(result);
            var exception = Assert.Throws<Exception>(() => repository.FindByMesAno(data, idUsuario));
            Assert.Equal("LancamentoRepositorioImpl_FindByMesAno_Erro", exception.Message);
        }

        [Fact, Order(4)]
        public void FindByMesAno_Throws_Exception_When_Receita_Execute_Where()
        {
            // Arrange
            var data = _mockAnoMes;
            var idUsuario = 20;

            var receitaDbSetMock = new Mock<DbSet<Receita>>();
            receitaDbSetMock.As<IQueryable<Receita>>()
               .Setup(d => d.Provider)
               .Throws<Exception>();

            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "FindByMesAno Throws Exception When Receita Execute Where   ")
                .Options;

            var context = new RegisterContext(options);
            context.Receita = receitaDbSetMock.Object;


            var repository = new LancamentoRepositorioImpl(context);

            // Act
            Action result = () => repository.FindByMesAno(data, idUsuario);

            // Assert
            Assert.NotNull(result);
            var exception = Assert.Throws<Exception>(() => repository.FindByMesAno(data, idUsuario));
            Assert.Equal("LancamentoRepositorioImpl_FindByMesAno_Erro", exception.Message);
        }
    }
}