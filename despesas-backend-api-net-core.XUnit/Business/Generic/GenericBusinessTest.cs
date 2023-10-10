using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Test.XUnit.Business.Generic
{
    public class GenericBusinessTests
    {
        [Fact]
        public void Create_Should_Return_Inserted_Object()
        {
            // Arrange
            var obj = CategoriaFaker.Categorias().First();
            var repositoryMock = new Mock<IRepositorio<Categoria>>();
            repositoryMock.Setup(repo => repo.Insert(obj)).Returns(obj);
            var business = new GenericBusiness<Categoria>(repositoryMock.Object);

            // Act
            var result = business.Create(obj);

            // Assert
            Assert.Equal(obj, result);
        }

        [Fact]
        public void FindAll_Should_Return_All_Objects()
        {
            // Arrange
            var objects = UsuarioFaker.Usuarios();
            var repositoryMock = new Mock<IRepositorio<Usuario>>();
            repositoryMock.Setup(repo => repo.GetAll()).Returns(objects);
            var business = new GenericBusiness<Usuario>(repositoryMock.Object);

            // Act
            var result = business.FindAll(1);

            // Assert
            Assert.Equal(objects, result);
        }

        [Fact]
        public void FindById_Should_Return_Object_With_MatchingId()
        {
            // Arrange
            var id = 1;
            var obj = DespesaFaker.Despesas().First();
            var repositoryMock = new Mock<IRepositorio<Despesa>>();
            repositoryMock.Setup(repo => repo.Get(id)).Returns(obj);
            var business = new GenericBusiness<Despesa>(repositoryMock.Object);

            // Act
            var result = business.FindById(id, 1);

            // Assert
            Assert.Equal(obj, result);
        }

        [Fact]
        public void Update_Should_Return_Updated_Object()
        {
            // Arrange
            var obj = ReceitaFaker.Receitas().First() ;
            var repositoryMock = new Mock<IRepositorio<Receita>>();
            repositoryMock.Setup(repo => repo.Update(obj)).Returns(obj);
            var business = new GenericBusiness<Receita>(repositoryMock.Object);

            // Act
            var result = business.Update(obj);

            // Assert
            Assert.Equal(obj, result);
        }

        [Fact]
        public void Delete_Should_Return_True_If_Deleted_Successfully()
        {
            // Arrange
            var objects = UsuarioFaker.Usuarios();
            var obj = objects.First();
            var repositoryMock = Usings.MockRepositorio(objects);
            repositoryMock.Setup(repo => repo.Delete(obj)).Returns(true);
            var business = new GenericBusiness<Usuario>(repositoryMock.Object);

            // Act
            var result = business.Delete(obj);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void FindByIdUsuario_Should_Return_EmptyList()
        {
            // Arrange
            var idUsuario = 1;
            var business = new GenericBusiness<BaseModel>(Mock.Of<IRepositorio<BaseModel>>());

            // Act
            var result = business.FindByIdUsuario(idUsuario);

            // Assert
            Assert.Empty(result);
        }
    }
}

