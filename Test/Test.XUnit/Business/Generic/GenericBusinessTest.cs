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
        public void Create_ShouldReturnInsertedObject()
        {
            // Arrange
            var obj = new BaseModel();
            var repositoryMock = new Mock<IRepositorio<BaseModel>>();
            repositoryMock.Setup(repo => repo.Insert(obj)).Returns(obj);
            var business = new GenericBusiness<BaseModel>(repositoryMock.Object);

            // Act
            var result = business.Create(obj);

            // Assert
            Assert.Equal(obj, result);
        }

        [Fact]
        public void FindAll_ShouldReturnAllObjects()
        {
            // Arrange
            int idUsuario = 1;
            var objects = new List<BaseModel> { new BaseModel(), new BaseModel() };
            var repositoryMock = new Mock<IRepositorio<BaseModel>>();
            repositoryMock.Setup(repo => repo.GetAll()).Returns(objects);
            var business = new GenericBusiness<BaseModel>(repositoryMock.Object);

            // Act
            var result = business.FindAll(idUsuario);

            // Assert
            Assert.Equal(objects, result);
        }

        [Fact]
        public void FindById_ShouldReturnObjectWithMatchingId()
        {
            // Arrange
            int idUsuario = 1;
            var id = 1;
            var obj = new BaseModel { Id = id };
            var repositoryMock = new Mock<IRepositorio<BaseModel>>();
            repositoryMock.Setup(repo => repo.Get(id)).Returns(obj);
            var business = new GenericBusiness<BaseModel>(repositoryMock.Object);

            // Act
            var result = business.FindById(id, idUsuario);

            // Assert
            Assert.Equal(obj, result);
        }

        [Fact]
        public void Update_ShouldReturnUpdatedObject()
        {
            // Arrange
            var obj = new BaseModel();
            var repositoryMock = new Mock<IRepositorio<BaseModel>>();
            repositoryMock.Setup(repo => repo.Update(obj)).Returns(obj);
            var business = new GenericBusiness<BaseModel>(repositoryMock.Object);

            // Act
            var result = business.Update(obj);

            // Assert
            Assert.Equal(obj, result);
        }

        [Fact]
        public void Delete_ShouldReturnTrueIfDeletedSuccessfully()
        {
            // Arrange
            var id = 1;
            var repositoryMock = new Mock<IRepositorio<BaseModel>>();
            repositoryMock.Setup(repo => repo.Delete(id)).Returns(true);
            var business = new GenericBusiness<BaseModel>(repositoryMock.Object);

            // Act
            var result = business.Delete(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void FindByIdUsuario_ShouldReturnEmptyList()
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

