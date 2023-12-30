using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit.Extensions.Ordering;

namespace Infrastructure.EntityConfig
{
    [Order(206)]
    public class LancamentoMapTest
    {
        [Fact]
        public void EntityConfiguration_IsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using (var context = new RegisterContext(options))
            {
                var builder = new ModelBuilder(new ConventionSet());
                var configuration = new LancamentoMap();

                configuration.Configure(builder.Entity<Lancamento>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Lancamento));

                // Act

                var idProperty = entityType.FindProperty("Id");

                var usuarioIdProperty = entityType.FindProperty("UsuarioId");
                var despesaIdProperty = entityType.FindProperty("DespesaId");
                var receitaIdProperty = entityType.FindProperty("ReceitaId");
                var dataProperty = entityType.FindProperty("Data");
                var dataCriacaoProperty = entityType.FindProperty("DataCriacao");
                var valorProperty = entityType.FindProperty("Valor");
                var descricaoProperty = entityType.FindProperty("Descricao");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(usuarioIdProperty);
                Assert.NotNull(despesaIdProperty);
                Assert.NotNull(receitaIdProperty);
                Assert.NotNull(dataProperty);
                Assert.NotNull(dataCriacaoProperty);
                Assert.NotNull(valorProperty);
                Assert.NotNull(descricaoProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(usuarioIdProperty.IsNullable);
                Assert.True(despesaIdProperty.IsNullable);
                Assert.Null(despesaIdProperty.GetDefaultValue());
                Assert.True(receitaIdProperty.IsNullable);
                Assert.Null(receitaIdProperty.GetDefaultValue());
                Assert.True(dataProperty.GetColumnType() == "timestamp");
                Assert.False(dataProperty.IsNullable);
                Assert.True(dataCriacaoProperty.GetColumnType() == "timestamp");
                //Assert.Equal(DateTime.Now, dataCriacaoProperty.GetDefaultValue());
                Assert.True(valorProperty.GetColumnType() == "decimal(10, 2)");
                Assert.Equal(100, descricaoProperty.GetMaxLength());
            }
        }

        [Fact]
        public void Should_Parse_LancamentoVM_To_Lancamento()
        {
            // Arrange
            var lancamentoMap = new LancamentoMap();
            var lancamentoVM = new LancamentoVM
            {
                Id = 1,
                IdUsuario = 1,
                IdDespesa = 1,
                IdReceita = 0,
                Valor = 2000,
                Data = DateTime.Now.ToDateBr(),
                Descricao = "LancamentoVM Teste",
                TipoCategoria = "Despesa",
                Categoria = Mock.Of<Categoria>().Descricao,
            };

            // Act
            var lancamento = lancamentoMap.Parse(lancamentoVM);

            // Assert
            Assert.Equal(lancamentoVM.Id, lancamento.Id);
            Assert.Equal(lancamentoVM.IdUsuario, lancamento.UsuarioId);
            Assert.Equal(lancamentoVM.IdDespesa, lancamento.DespesaId);
            Assert.Equal(lancamentoVM.IdReceita, lancamento.ReceitaId);
            Assert.Equal(lancamentoVM.Valor, lancamento.Valor);
            Assert.Equal(lancamentoVM.Data, lancamento.Data.ToDateBr());
        }

        [Fact]
        public void Should_Parse_Lancamento_To_LancamentoVM()
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
            Assert.Equal(lancamentoVM.Valor, lancamento.Valor);
            Assert.Equal(lancamentoVM.Data, lancamento.Data.ToString("dd/MM/yyyy"));
            //Assert.Equal(lancamentoVM.Descricao, lancamento.Descricao);
        }

        [Fact]
        public void Should_Parse_List_LancamentoVM_To_Lancamento()
        {
            // Arrange
            var lancamentoMap = new LancamentoMap();
            var lancamentoVMs = new List<LancamentoVM>
            {
                new LancamentoVM
                {
                    Id = 1,
                    IdUsuario = 1,
                    IdDespesa = 1,
                    IdReceita = 0,
                    Valor = 2000,
                    Data = DateTime.Now.ToString("dd/MM/yyyy"),
                    Descricao = "LancamentoVM Teste",
                    TipoCategoria = "Despesa",
                    Categoria = Mock.Of<Categoria>().Descricao,
                },
                new LancamentoVM
                {
                    Id = 2,
                    IdUsuario = 3,
                    IdDespesa = 0,
                    IdReceita = 1,
                    Valor = 500,
                    Data = DateTime.Now.ToString("dd/MM/yyyy"),
                    Descricao = "LancamentoVM Teste",
                    TipoCategoria = "Receita",
                    Categoria = Mock.Of<Categoria>().Descricao,
                },
                new LancamentoVM
                {
                    Id = 3,
                    IdUsuario = 2,
                    IdDespesa = 1,
                    IdReceita = 0,
                    Valor = 70000,
                    Data = DateTime.Now.ToString("dd/MM/yyyy"),
                    Descricao = "LancamentoVM Teste",
                    TipoCategoria = "Despesa",
                    Categoria = Mock.Of<Categoria>().Descricao,
                }
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
        public void Should_Parse_Parse_List_Lancamento_To_lancamentoVM()
        {
            // Arrange
            var lancamentoMap = new LancamentoMap();
            var lancamentos = new List<Lancamento>
            {
                new Lancamento
                {
                    Id = 1,
                    UsuarioId = 1,
                    DespesaId = 1,
                    ReceitaId = 0,
                    Valor = 2000,
                    Data = DateTime.Now,
                    Descricao = "Lancamento Teste 1",
                    DataCriacao = DateTime.Now,
                    Categoria = Mock.Of<Categoria>()
                },
                new Lancamento
                {
                    Id = 3,
                    UsuarioId = 3,
                    DespesaId = 0,
                    ReceitaId = 1,
                    Valor = 20,
                    Data = DateTime.Now,
                    Descricao = "Lancamento Teste 2",
                    DataCriacao = DateTime.Now,
                    Categoria = Mock.Of<Categoria>()
                },
                new Lancamento
                {
                    Id = 3,
                    UsuarioId = 2,
                    DespesaId = 1,
                    ReceitaId = 0,
                    Valor = 0,
                    Data = DateTime.Now,
                    Descricao = "Lancamento Teste 3",
                    DataCriacao = DateTime.Now,
                    Categoria = Mock.Of<Categoria>()
                }
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

        [Fact]
        public void Should_Parse_Despesa_To_Lancamento()
        {
            // Arrange
            var map = new LancamentoMap();
            var usuario = UsuarioFaker.GetNewFaker(1);
            var origin = DespesaFaker.GetNewFaker(
                usuario,
                CategoriaFaker.GetNewFaker(usuario, TipoCategoria.Despesa, usuario.Id)
            );

            // Act
            var result = map.Parse(origin);

            // Assert
            Assert.Equal(origin.Valor, result.Valor);
            Assert.Equal(origin.Descricao, result.Descricao);
            Assert.Equal(origin.UsuarioId, result.UsuarioId);
            Assert.Equal(origin.Usuario, result.Usuario);
            Assert.Equal(origin.Id, result.DespesaId);
            Assert.Equal(origin, result.Despesa);
            Assert.Equal(0, result.ReceitaId);
            Assert.NotNull(result.Receita);
            Assert.IsType<Receita>(result.Receita);
            Assert.Equal(origin.CategoriaId, result.CategoriaId);
            Assert.Equal(origin.Categoria, result.Categoria);
        }

        [Fact]
        public void Should_Parse_Receita_To_Lancamento()
        {
            // Arrange
            var map = new LancamentoMap();
            var usuario = UsuarioFaker.GetNewFaker(1);
            var origin = ReceitaFaker.GetNewFaker(
                usuario,
                CategoriaFaker.GetNewFaker(usuario, TipoCategoria.Receita, usuario.Id)
            );

            // Act
            var result = map.Parse(origin);

            // Assert
            Assert.Equal(origin.Valor, result.Valor);
            Assert.Equal(origin.Descricao, result.Descricao);
            Assert.Equal(origin.UsuarioId, result.UsuarioId);
            Assert.Equal(origin.Usuario, result.Usuario);
            Assert.Equal(0, result.DespesaId);
            Assert.NotNull(result.Despesa);
            Assert.IsType<Despesa>(result.Despesa);
            Assert.Equal(origin.Id, result.ReceitaId);
            Assert.Equal(origin, result.Receita);
            Assert.Equal(origin.CategoriaId, result.CategoriaId);
            Assert.Equal(origin.Categoria, result.Categoria);
        }

        [Fact]
        public void Should_Parse_List_Despesas_To_List_Lancamentos()
        {
            // Arrange
            var map = new LancamentoMap();
            var usuarario = UsuarioFaker.GetNewFaker(1);
            var origin = DespesaFaker.Despesas(usuarario, usuarario.Id);

            // Act
            var result = map.ParseList(origin);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(origin.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(origin[i].Valor, result[i].Valor);
                Assert.Equal(origin[i].Descricao, result[i].Descricao);
                Assert.Equal(origin[i].UsuarioId, result[i].UsuarioId);
                Assert.Equal(origin[i].Usuario, result[i].Usuario);
                Assert.Equal(origin[i].Id, result[i].DespesaId);
                Assert.Equal(origin[i], result[i].Despesa);
                Assert.Equal(0, result[i].ReceitaId);
                Assert.NotNull(result[i].Receita);
                Assert.IsType<Receita>(result[i].Receita);
                Assert.Equal(origin[i].CategoriaId, result[i].CategoriaId);
                Assert.Equal(origin[i].Categoria, result[i].Categoria);
            }
        }

        [Fact]
        public void Should_Parse_List_Receitas_To_List_Lancamentos()
        {
            // Arrange
            var map = new LancamentoMap();
            var usuarario = UsuarioFaker.GetNewFaker(1);
            var origin = ReceitaFaker.Receitas(usuarario, usuarario.Id);

            // Act
            var result = map.ParseList(origin);

            Assert.NotNull(result);
            Assert.Equal(origin.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(origin[i].Valor, result[i].Valor);
                Assert.Equal(origin[i].Descricao, result[i].Descricao);
                Assert.Equal(origin[i].UsuarioId, result[i].UsuarioId);
                Assert.Equal(origin[i].Usuario, result[i].Usuario);
                Assert.Equal(0, result[i].DespesaId);
                Assert.NotNull(result[i].Despesa);
                Assert.IsType<Despesa>(result[i].Despesa);
                Assert.Equal(origin[i].Id, result[i].ReceitaId);
                Assert.Equal(origin[i], result[i].Receita);
                Assert.Equal(origin[i].CategoriaId, result[i].CategoriaId);
                Assert.Equal(origin[i].Categoria, result[i].Categoria);
            }
        }
    }
}
