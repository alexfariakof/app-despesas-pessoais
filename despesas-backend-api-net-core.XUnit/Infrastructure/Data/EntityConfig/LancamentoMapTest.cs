using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;

namespace Test.XUnit.Infrastructure.Data.EntityConfig
{
    public class LancamentoMapTest
    {
        [Fact]
        public void LancamentoMap_Should_Parse_LancamentoVM_To_Lancamento()
        {
            // Arrange
            var lancamentoMap = new LancamentoMap();
            var lancamentoVM = new LancamentoVM
            {
                Id = 1,
                IdUsuario = 1,
                IdDespesa = 1,
                IdReceita  = 0,
                Valor = "2000",
                Data = DateTime.Now.ToString("dd/MM/yyyy"),
                Descricao = "LancamentoVM Teste",
                TipoCategoria = "Despesa",
                Categoria = Mock.Of<Categoria>(),                
            };

            // Act
            var lancamento = lancamentoMap.Parse(lancamentoVM);

            // Assert
            Assert.Equal(lancamentoVM.Id, lancamento.Id);
            Assert.Equal(lancamentoVM.IdUsuario, lancamento.UsuarioId);
            Assert.Equal(lancamentoVM.IdDespesa, lancamento.DespesaId);
            Assert.Equal(lancamentoVM.IdReceita, lancamento.ReceitaId);
            Assert.Equal(lancamentoVM.Valor, lancamento.Valor.ToString());
            Assert.Equal(lancamentoVM.Data, lancamento.Data.ToString("dd/MM/yyyy"));
        }

        [Fact]
        public void LancamentoMap_Should_Parse_Lancamento_To_LancamentoVM()
        {
            // Arrange
            var lancamentoMap = new LancamentoMap();
            var lancamento = new Lancamento
            {
                Id = 1,
                UsuarioId = 1,
                DespesaId = 1,
                ReceitaId = 0,
                Valor = 2000,
                Data = DateTime.Now,
                Descricao = "Lancamento Teste",
                DataCriacao = DateTime.Now,
                Categoria = Mock.Of<Categoria>()
            };

            // Act
            var lancamentoVM = lancamentoMap.Parse(lancamento);

            // Assert
            // Assert
            Assert.Equal(lancamentoVM.Id, lancamento.Id);
            Assert.Equal(lancamentoVM.IdUsuario, lancamento.UsuarioId);
            Assert.Equal(lancamentoVM.IdDespesa, lancamento.DespesaId);
            Assert.Equal(lancamentoVM.IdReceita, lancamento.ReceitaId);
            Assert.Equal(lancamentoVM.Valor, lancamento.Valor.ToString("N2"));
            Assert.Equal(lancamentoVM.Data, lancamento.Data.ToString("dd/MM/yyyy"));
            //Assert.Equal(lancamentoVM.Descricao, lancamento.Descricao);
        }

        [Fact]
        public void LancamentoMap_Should_ParseList_LancamentoVM_To_Lancamento()
        {
            // Arrange
            var lancamentoMap = new LancamentoMap();
            var lancamentoVMs = new List<LancamentoVM>
            {
                new LancamentoVM { Id = 1,IdUsuario = 1, IdDespesa = 1, IdReceita  = 0, Valor = "2000", Data = DateTime.Now.ToString("dd/MM/yyyy"), Descricao = "LancamentoVM Teste", TipoCategoria = "Despesa", Categoria = Mock.Of<Categoria>(), },
                new LancamentoVM { Id = 2,IdUsuario = 3, IdDespesa = 0, IdReceita  = 1, Valor = "500", Data = DateTime.Now.ToString("dd/MM/yyyy"), Descricao = "LancamentoVM Teste", TipoCategoria = "Receita", Categoria = Mock.Of<Categoria>(), },
                new LancamentoVM { Id = 3,IdUsuario = 2, IdDespesa = 1, IdReceita  = 0, Valor = "70000", Data = DateTime.Now.ToString("dd/MM/yyyy"), Descricao = "LancamentoVM Teste", TipoCategoria = "Despesa", Categoria = Mock.Of<Categoria>(), }
            };

            // Act
            var lancamentos = lancamentoMap.ParseList(lancamentoVMs);

            // Assert
            Assert.Equal(lancamentoVMs.Count, lancamentos.Count);
            for (int i = 0; i < lancamentoVMs.Count; i++)
            {
                Assert.Equal(lancamentoVMs[i].Id, lancamentos[i].Id);
                //Assert.Equal(lancamentoVMs[i].Descricao, lancamentos[i].Descricao);
                Assert.Equal(lancamentoVMs[i].IdUsuario, lancamentos[i].UsuarioId);                
            }
        }

        [Fact]
        public void LancamentoMap_Should_ParseList_Lancamento_To_lancamentoVM()
        {
            // Arrange
            var lancamentoMap = new LancamentoMap();
            var lancamentos = new List<Lancamento>
            {
                new Lancamento { Id = 1, UsuarioId = 1, DespesaId = 1, ReceitaId = 0, Valor = 2000, Data = DateTime.Now, Descricao = "Lancamento Teste 1", DataCriacao = DateTime.Now,Categoria = Mock.Of<Categoria>() },
                new Lancamento { Id = 3, UsuarioId = 3, DespesaId = 0, ReceitaId = 1, Valor = 20, Data = DateTime.Now, Descricao = "Lancamento Teste 2", DataCriacao = DateTime.Now,Categoria = Mock.Of<Categoria>() },
                new Lancamento { Id = 3, UsuarioId = 2, DespesaId = 1, ReceitaId = 0, Valor = 0, Data = DateTime.Now, Descricao = "Lancamento Teste 3", DataCriacao = DateTime.Now,Categoria = Mock.Of<Categoria>() }
            };

            // Act
            var lancamentoVMs = lancamentoMap.ParseList(lancamentos);

            // Assert
            Assert.Equal(lancamentos.Count, lancamentoVMs.Count);
            for (int i = 0; i < lancamentos.Count; i++)
            {
                Assert.Equal(lancamentos[i].Id, lancamentoVMs[i].Id);
                //Assert.Equal(lancamentos[i].Descricao, lancamentoVMs[i].Descricao);
                Assert.Equal(lancamentos[i].UsuarioId, lancamentoVMs[i].IdUsuario);
            }
        }
    }
}