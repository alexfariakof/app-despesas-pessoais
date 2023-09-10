using System.Linq.Expressions;

namespace Test.XUnit.Infrastructure.Data.Repositories.Implementations
{
    public class ControleAcessoRepositorioImplTest
    {
        private readonly Mock<RegisterContext> contextMock;
        private readonly ControleAcessoRepositorioImpl repository;
        private static DbSet<T> MockDbSet<T>(List<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSetMock.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => data.Add(s));
            return dbSetMock.Object;
        }

        public ControleAcessoRepositorioImplTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            contextMock = new Mock<RegisterContext>(options);
            repository = new ControleAcessoRepositorioImpl(contextMock.Object);
        }

        // ...

        [Fact]
        public void Create_WithNewControleAcesso_ShouldCreateControleAcessoAndUsuarioAndCategorias()
        {
            // Arrange
            var controleAcesso = new ControleAcesso
            {
                Login = "testuser",
                Senha = "testpassword",
                Usuario = new Usuario
                {
                    Nome = "testuser",
                    SobreNome = "Sobre nome Tester",
                    Telefone = "(21) 00000-0000",
                    Email = "testuser"
                }
            };

            var dsUsuarioMock = new Mock<DbSet<Usuario>>();
            var dsControleAcessoMock = new Mock<DbSet<ControleAcesso>>();
            var dsCategoriaMock = new Mock<DbSet<Categoria>>();

            dsUsuarioMock.Setup(d => d.Add(It.IsAny<Usuario>()));
            dsControleAcessoMock.Setup(d => d.Add(It.IsAny<ControleAcesso>()));
            dsCategoriaMock.Setup(d => d.Add(It.IsAny<Categoria>())).Verifiable();

            contextMock.Setup(c => c.Set<Usuario>()).Returns(dsUsuarioMock.Object);
            contextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dsControleAcessoMock.Object);
            contextMock.Setup(c => c.Set<Categoria>()).Returns(dsCategoriaMock.Object);

            // Act
            var result = repository.Create(controleAcesso);

            // Assert
            //dsUsuarioMock.Verify(d => d.Add(controleAcesso.Usuario), Times.Once);
            //dsControleAcessoMock.Verify(d => d.Add(controleAcesso), Times.Once);
            //dsCategoriaMock.Verify(d => d.Add(It.IsAny<Categoria>()), Times.Exactly(13)); // Verify 13 categories are added

            contextMock.Verify(c => c.SaveChanges(), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void Create_WithExistingControleAcesso_ShouldNotCreateControleAcessoAndUsuarioAndCategorias()
        {
            var controleAcesso = new ControleAcesso
            {
                Login = "testuser",
                Senha = "testpassword",
                Usuario = new Usuario
                {
                    Nome = "testuser",
                    SobreNome = "Sobre nome Tester",
                    Telefone = "(21) 00000-0000",
                    Email = "testuser"
                }
            };

            var dsControleAcessoMock = new Mock<DbSet<ControleAcesso>>();

            dsControleAcessoMock.Setup(d => d.Add(It.IsAny<ControleAcesso>()));
            contextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dsControleAcessoMock.Object);
            contextMock.Setup(c => c.ControleAcesso).Returns(MockDbSet(new List<ControleAcesso> { controleAcesso }));

            // Act
            var result = repository.Create(controleAcesso);

            // Assert
            dsControleAcessoMock.Verify(d => d.Add(controleAcesso), Times.Never);
            contextMock.Verify(c => c.SaveChanges(), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public void FindByEmail_WithExistingEmail_ShouldReturnControleAcesso()
        {
            // Arrange
            var existingEmail = "testuser@example.com";
            var existingControleAcesso = new ControleAcesso { Login = existingEmail };
            var dataSetControleAcesso = new List<ControleAcesso> { existingControleAcesso };
            var dbSetControleAcessoMock = MockDbSet(dataSetControleAcesso);
            contextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dbSetControleAcessoMock);

            // Act
            var result = repository.FindByEmail(new ControleAcesso { Login = existingEmail });

            // Assert
            Assert.Equal(existingControleAcesso, result);
        }

        [Fact]
        public void FindByEmail_WithNonExistingEmail_ShouldReturnNull()
        {
            // Arrange
            var nonExistingEmail = "nonexistinguser@example.com";
            var dataSetControleAcesso = new List<ControleAcesso>();
            var dbSetControleAcessoMock = MockDbSet(dataSetControleAcesso);
            contextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dbSetControleAcessoMock);

            // Act
            var result = repository.FindByEmail(new ControleAcesso { Login = nonExistingEmail });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetUsuarioByEmail_WithExistingEmail_ShouldReturnUsuario()
        {
            // Arrange
            var existingEmail = "testuser@example.com";
            var existingUsuario = new Usuario { Email = existingEmail };
            var dataSetUsuario = new List<Usuario> { existingUsuario };
            var dbSetUsuarioMock = MockDbSet(dataSetUsuario);
            contextMock.Setup(c => c.Set<Usuario>()).Returns(dbSetUsuarioMock);

            // Act
            var result = repository.GetUsuarioByEmail(existingEmail);

            // Assert
            Assert.Equal(existingUsuario, result);
        }

        [Fact]
        public void GetUsuarioByEmail_WithNonExistingEmail_ShouldReturnNull()
        {
            // Arrange
            var nonExistingEmail = "nonexistinguser@example.com";
            var dataSetUsuario = new Mock<List<Usuario>>().Object;
            var dbSetUsuarioMock = MockDbSet(dataSetUsuario);
            contextMock.Setup(c => c.Set<Usuario>()).Returns(dbSetUsuarioMock);

            // Act
            var result = repository.GetUsuarioByEmail(nonExistingEmail);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void RecoveryPassword_WithExistingEmail_ShouldUpdateControleAcessoAndSendEmail()
        {
            // Arrange
            var existingEmail = "testuser@example.com";
            var existingControleAcesso = new ControleAcesso { Login = existingEmail, Senha = "oldpassword" };
            var dataSetControleAcesso = new List<ControleAcesso> { existingControleAcesso };
            var dbSetControleAcessoMock = MockDbSet(dataSetControleAcesso);
            contextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dbSetControleAcessoMock);
            contextMock.Setup(c => c.ControleAcesso).Returns(dbSetControleAcessoMock);

            var dsControleAcessoMock = new Mock<DbSet<ControleAcesso>>();
            dsControleAcessoMock.Setup(d => d.SingleOrDefault(It.IsAny <Expression<Func<ControleAcesso, bool>>> ()))
                .Returns(existingControleAcesso);

            contextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dsControleAcessoMock.Object);


            // Act
            var result = repository.RecoveryPassword(existingEmail);

            // Assert
            dsControleAcessoMock.Verify(d => d.SingleOrDefault(It.IsAny < Expression < Func<ControleAcesso,bool>>> ()), Times.Once);
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
            Assert.True(result);
            Assert.NotEqual("oldpassword", existingControleAcesso.Senha);
        }

        [Fact]
        public void RecoveryPassword_WithNonExistingEmail_ShouldReturnFalse()
        {
            // Arrange
            var nonExistingEmail = "nonexistinguser@example.com";
            var dataSetControleAcesso = new List<ControleAcesso>();
            var dbSetControleAcessoMock = MockDbSet(dataSetControleAcesso);
            contextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dbSetControleAcessoMock);

            // Act
            var result = repository.RecoveryPassword(nonExistingEmail);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ChangePassword_WithExistingIdUsuario_ShouldUpdateControleAcessoAndUsuario()
        {
            // Arrange
            var existingIdUsuario = 1;
            var existingUsuario = new Usuario { Id = existingIdUsuario };
            var existingControleAcesso = new ControleAcesso { Login = "testuser", Senha = "oldpassword", Usuario = existingUsuario };
            var dataSetUsuario = new List<Usuario> { existingUsuario };
            var dataSetControleAcesso = new List<ControleAcesso> { existingControleAcesso };
            var dbSetUsuarioMock = MockDbSet(dataSetUsuario);
            var dbSetControleAcessoMock = MockDbSet(dataSetControleAcesso);
            contextMock.Setup(c => c.Set<Usuario>()).Returns(dbSetUsuarioMock);
            contextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dbSetControleAcessoMock);

            var dsControleAcessoMock = new Mock<DbSet<ControleAcesso>>();
            var dsUsuarioMock = new Mock<DbSet<Usuario>>();

            dsControleAcessoMock.Setup(d => d.SingleOrDefault(It.IsAny <Expression <Func<ControleAcesso, bool>>> ()))
                .Returns(existingControleAcesso);
            dsUsuarioMock.Setup(d => d.SingleOrDefault(It.IsAny <Expression < Func<Usuario, bool>>> ()))
                .Returns(existingUsuario);

            contextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dsControleAcessoMock.Object);
            contextMock.Setup(c => c.Set<Usuario>()).Returns(dsUsuarioMock.Object);

            // Act
            var result = repository.ChangePassword(existingIdUsuario, "newpassword");

            // Assert
            dsControleAcessoMock.Verify(d => d.SingleOrDefault(It.IsAny <Expression<Func<ControleAcesso, bool>>> ()), Times.Once);
            dsUsuarioMock.Verify(d => d.SingleOrDefault(It.IsAny <Expression <Func<Usuario, bool>>> ()), Times.Once);
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
            Assert.True(result);
            Assert.NotEqual("oldpassword", existingControleAcesso.Senha);
            Assert.Equal(StatusUsuario.Ativo, existingUsuario.StatusUsuario);
        }

        [Fact]
        public void ChangePassword_WithNonExistingIdUsuario_ShouldThrowException()
        {
            // Arrange
            var nonExistingIdUsuario = 999;
            var dataSetUsuario = new List<Usuario>();
            var dbSetUsuarioMock = MockDbSet(dataSetUsuario);
            contextMock.Setup(c => c.Set<Usuario>()).Returns(dbSetUsuarioMock);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => repository.ChangePassword(nonExistingIdUsuario, "newpassword"));
        }
    }
}
