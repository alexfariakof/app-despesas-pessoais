using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit.Extensions.Ordering;

namespace Test.XUnit.Controllers
{
    [Order(12)]
    public class ReceitaControllerTest
    {
        protected Mock<IBusiness<ReceitaVM>> _mockReceitaBusiness;
        protected ReceitaController _receitaController;
        protected List<ReceitaVM> _receitaVMs;
        private void SetupBearerToken(int idUsuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
            };
            var identity = new ClaimsIdentity(claims, "IdUsuario");
            var claimsPrincipal = new ClaimsPrincipal(identity);


            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };
            httpContext.Request.Headers["Authorization"] = "Bearer " + Usings.GenerateJwtToken(idUsuario);

            _receitaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        public ReceitaControllerTest()
        {
            
            _mockReceitaBusiness = new Mock<IBusiness<ReceitaVM>>();
            _receitaController = new ReceitaController(_mockReceitaBusiness.Object);
            _receitaVMs = ReceitaFaker.ReceitasVMs();    
        }

        [Fact, Order(1)]
        public void Get_Should_Return_All_Receitas_From_Usuario()
        {
            // Arrange
            int idUsuario = _receitaVMs.First().IdUsuario;
            SetupBearerToken(idUsuario);
            _mockReceitaBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_receitaVMs);

            // Act
            var result = _receitaController.Get() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_receitaVMs, result.Value);
            _mockReceitaBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
        }

        [Fact, Order(2)]
        public void GetById_Should_Returns_BadRequest_When_Receita_NULL()
        {
            // Arrange
            var receitaVM = _receitaVMs.First();
            var idUsuario = receitaVM.IdUsuario;
            SetupBearerToken(idUsuario);
            _mockReceitaBusiness.Setup(business => business.FindById(receitaVM.Id, idUsuario)).Returns((ReceitaVM)null);

            // Act
            var result = _receitaController.GetById(receitaVM.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Nenhuma receita foi encontrada.", message);
            _mockReceitaBusiness.Verify(b => b.FindById(receitaVM.Id, idUsuario), Times.Once);
        }

        [Fact, Order(3)]
        public void GetById_Should_Returns_OkResults_With_Despesas()
        {
            // Arrange
            var receita = _receitaVMs.Last();
            int idUsuario = receita.IdUsuario;
            int receitaId = receita.Id;
            SetupBearerToken(idUsuario);

            _mockReceitaBusiness.Setup(business => business.FindById(receitaId, idUsuario)).Returns(receita);

            // Act
            var result = _receitaController.GetById(receitaId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
            var _receita = (ReceitaVM)value.GetType().GetProperty("receita").GetValue(value, null);
            Assert.NotNull(_receita);
            Assert.IsType<ReceitaVM>(_receita);
            _mockReceitaBusiness.Verify(b => b.FindById(receitaId, idUsuario), Times.Once);
        }

        [Fact, Order(4)]
        public void GetById_Should_Returns_BadRequest_When_Throws_Error()
        {
            // Arrange
            var receitaVM = _receitaVMs.First();
            var idUsuario = receitaVM.IdUsuario;
            SetupBearerToken(idUsuario);
            _mockReceitaBusiness.Setup(business => business.FindById(receitaVM.Id, idUsuario)).Throws(new Exception());

            // Act
            var result = _receitaController.GetById(receitaVM.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Não foi possível realizar a consulta da receita.", message);
            _mockReceitaBusiness.Verify(b => b.FindById(receitaVM.Id, idUsuario), Times.Once);
        }
        
        [Fact, Order(5)]
        public void GetByIdUsuario_Should_Returns_OkResult_With_Receitas()
        {
            // Arrange
            var receita = _receitaVMs.Last();
            int idUsuario = receita.IdUsuario;
            int receitaId = receita.Id;
            SetupBearerToken(idUsuario);

            _mockReceitaBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_receitaVMs.FindAll(r => r.IdUsuario == idUsuario));

            // Act
            var result = _receitaController.Post(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var _receita = (List<ReceitaVM>)result.Value;
            Assert.NotNull(_receita);
            Assert.IsType<List<ReceitaVM>>(_receita);
            _mockReceitaBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
        }

        [Fact, Order(6)]
        public void GetByIdUsuario_Should_Returns_BadRequest_For_Invalid_UserId()
        {
            // Arrange
            int idUsuario = _receitaVMs.Last().Id;
            SetupBearerToken(2);
            _mockReceitaBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_receitaVMs.FindAll(d => d.IdUsuario == idUsuario));

            // Act
            var result = _receitaController.Post(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockReceitaBusiness.Verify(b => b.FindAll(idUsuario), Times.Never);
        }

        [Fact, Order(7)]
        public void GetByIdUsuario_Should_Returns_BadRequest_For_UserId_0()
        {
            // Arrange
            var receita = _receitaVMs.Last();
            receita.IdUsuario = 0;
            int idUsuario = receita.IdUsuario;
            SetupBearerToken(idUsuario);
            _mockReceitaBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_receitaVMs.FindAll(d => d.IdUsuario == idUsuario));

            // Act
            var result = _receitaController.Post(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Usuário inexistente!", message);
            _mockReceitaBusiness.Verify(b => b.FindAll(idUsuario), Times.Never);
        }

        [Fact, Order(8)]
        public void Post_Should_Create_Receita()
        {
            // Arrange
            var receitaVM = _receitaVMs[3];
            int idUsuario = receitaVM.IdUsuario ;
            SetupBearerToken(idUsuario);
            _mockReceitaBusiness.Setup(business => business.Create(receitaVM)).Returns(receitaVM);

            // Act
            var result = _receitaController.Post(receitaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
            var _receita = (ReceitaVM)value.GetType().GetProperty("receita").GetValue(value, null);
            Assert.NotNull(_receita);
            Assert.IsType<ReceitaVM>(_receita);
            _mockReceitaBusiness.Verify(b => b.Create(receitaVM), Times.Once());
        }

        [Fact, Order(9)]
        public void Post_Should_Returns_BadRequest_When_Receita_With_InvalidUsuario()
        {
            // Arrange
            var receitaVM = _receitaVMs[3];
            int idUsuario = receitaVM.IdUsuario;
            SetupBearerToken(0);
            _mockReceitaBusiness.Setup(business => business.Create(receitaVM)).Returns(receitaVM);

            // Act
            var result = _receitaController.Post(receitaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockReceitaBusiness.Verify(b => b.Create(receitaVM), Times.Never);
        }

        [Fact, Order(10)]
        public void Post_Should_Returns_BadRequest_When_Throws_Error()
        {
            // Arrange
            var receitaVM = _receitaVMs[3];
            int idUsuario = receitaVM.IdUsuario;
            SetupBearerToken(idUsuario);
            _mockReceitaBusiness.Setup(business => business.Create(receitaVM)).Throws(new Exception());

            // Act
            var result = _receitaController.Post(receitaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Não foi possível realizar o cadastro da receita!", message);
            _mockReceitaBusiness.Verify(b => b.Create(receitaVM), Times.Once);
        }

        [Fact, Order(11)]
        public void Put_Should_Returns_BadRequest_When_Receita_With_InvalidUsuario()
        {
            // Arrange
            var receitaVM = _receitaVMs[3];
            int idUsuario = receitaVM.IdUsuario;
            SetupBearerToken(0);
            _mockReceitaBusiness.Setup(business => business.Update(receitaVM)).Returns(receitaVM);

            // Act
            var result = _receitaController.Put(receitaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockReceitaBusiness.Verify(b => b.Update(receitaVM), Times.Never);
        }

        [Fact, Order(12)]
        public void Put_Should_Update_Receita()
        {
            // Arrange
            var receitaVM = _receitaVMs[4];
            int idUsuario = receitaVM.IdUsuario;
            SetupBearerToken(idUsuario);         
            _mockReceitaBusiness.Setup(business => business.Update(receitaVM)).Returns(receitaVM);

            // Act
            var result = _receitaController.Put(receitaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
            var _receita = (ReceitaVM)value.GetType().GetProperty("receita").GetValue(value, null);
            Assert.NotNull(_receita);
            Assert.IsType<ReceitaVM>(_receita);
            _mockReceitaBusiness.Verify(b => b.Update(receitaVM), Times.Once);   
        }

        [Fact, Order(13)]
        public void Put_Should_Returns_BadRequest_When_Receita_Return_Null()
        {
            // Arrange
            var receitaVM = _receitaVMs[3];
            int idUsuario = receitaVM.IdUsuario;
            SetupBearerToken(idUsuario);
            _mockReceitaBusiness.Setup(business => business.Update(receitaVM)).Returns((ReceitaVM)null);

            // Act
            var result = _receitaController.Put(receitaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Não foi possível atualizar o cadastro da receita.", message);
            _mockReceitaBusiness.Verify(b => b.Update(receitaVM), Times.Once);
        }

        [Fact, Order(14)]
        public void Delete_Should_Returns_OkResult()
        {
            // Arrange
            var receitaVM = _receitaVMs[2];
            int idUsuario = receitaVM.IdUsuario;
            SetupBearerToken(idUsuario);
            
            _mockReceitaBusiness.Setup(business => business.Delete(receitaVM)).Returns(true);

            // Act
            var result = _receitaController.Delete(receitaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
            _mockReceitaBusiness.Verify(b => b.Delete(receitaVM), Times.Once);
        }

        [Fact, Order(15)]
        public void Delete_Should_Returns_BadRequest_With_InvalidUsuario()
        {
            // Arrange
            var receitaVM = _receitaVMs[2];
            int idUsuario = receitaVM.IdUsuario;
            SetupBearerToken(0);

            _mockReceitaBusiness.Setup(business => business.Delete(receitaVM)).Returns(true);

            // Act
            var result = _receitaController.Delete(receitaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockReceitaBusiness.Verify(b => b.Delete(receitaVM), Times.Never);
        }

        [Fact, Order(16)]
        public void Delete_Should_Returns_BadResquest_When_Receita_Not_Deleted()
        {
            // Arrange
            var receitaVM = _receitaVMs[2];
            int idUsuario = receitaVM.IdUsuario;
            SetupBearerToken(idUsuario);

            _mockReceitaBusiness.Setup(business => business.Delete(receitaVM)).Returns(false);

            // Act
            var result = _receitaController.Delete(receitaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Erro ao excluir Receita!", message);
            _mockReceitaBusiness.Verify(b => b.Delete(receitaVM), Times.Once);
        }
    }
}