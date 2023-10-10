using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Controllers;
using despesas_backend_api_net_core.Infrastructure.Security.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Test.XUnit.Controllers
{
    public class CategoriaControllerTests
    {
        private readonly Mock<IBusiness<CategoriaVM>> _mockCategoriaBusiness;
        private readonly CategoriaController _categoriaController;
        private readonly List<CategoriaVM> _categoriaVMs;

        public static string GenerateJwtToken(int userId)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var signingConfigurations = new SigningConfigurations();
            configuration.GetSection("TokenConfigurations").Bind(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();
            configuration.GetSection("TokenConfigurations").Bind(tokenConfigurations);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingConfigurations.Key.ToString()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("IdUsuario", userId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: tokenConfigurations.Issuer,
                audience: tokenConfigurations.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(tokenConfigurations.Seconds),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        private void SetupBearerToken(int userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };
            var identity = new ClaimsIdentity(claims, "IdUsuario");
            var claimsPrincipal = new ClaimsPrincipal(identity);


            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };
            httpContext.Request.Headers["Authorization"] = "Bearer " + GenerateJwtToken(userId);


            _categoriaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        public CategoriaControllerTests()
        {
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            _categoriaVMs = CategoriaFaker.CategoriasVMs();
        }

        [Fact]
        public void Get_Returns_Ok_Result()
        {
            // Arrange
            var categoriaVM = _categoriaVMs.First();
            SetupBearerToken(categoriaVM.IdUsuario);

            _mockCategoriaBusiness.Setup(b => b.FindAll(categoriaVM.IdUsuario)).Returns(_categoriaVMs);

            // Act
            var result = _categoriaController.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoriaVM>>(result.Value);
            var returnedResults = result.Value as List<CategoriaVM>;
            Assert.Equal(_categoriaVMs.Count, returnedResults.Count);
        }

        [Fact]
        public void GetById_ReturnsOkResult()
        {
            // Arrange
            var idCategoria = 1;
            var categoriaVM = _categoriaVMs.First();
            _mockCategoriaBusiness.Setup(b => b.FindById(idCategoria, categoriaVM.IdUsuario)).Returns(categoriaVM);

            // Act
            var result = _categoriaController.GetById(idCategoria) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoriaVM>(result.Value);
            Assert.Equal(categoriaVM, result.Value);
        }

        [Fact]
        public void GetById_ReturnsNotFound()
        {
            // Arrange
            var idCategoria = 100; // Assume a non-existent category ID
            var categoriaVM = _categoriaVMs.First();
            _mockCategoriaBusiness.Setup(b => b.FindById(idCategoria, categoriaVM.IdUsuario)).Returns((CategoriaVM)null);

            // Act
            var result = _categoriaController.GetById(idCategoria) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetByIdUsuario_ReturnsOkResult()
        {
            // Arrange
            var idUsuario = 1; // Assume a valid user ID
            _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(_categoriaVMs);

            // Act
            var result = _categoriaController.GetByIdUsuario(idUsuario) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoriaVM>>(result.Value);
            var returnedResults = result.Value as List<CategoriaVM>;
            Assert.Equal(_categoriaVMs.Count, returnedResults.Count);
        }

        [Fact]
        public void GetByIdUsuario_ReturnsBadRequest()
        {
            // Arrange
            var idUsuario = 1;
            var categoriaVM = _categoriaVMs.First();
            _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(new List<CategoriaVM>());

            // Act
            var result = _categoriaController.GetByIdUsuario(idUsuario) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetByTipoCategoria_ReturnsOkResult()
        {
            // Arrange
            var idUsuario = 1;
            var tipoCategoria = TipoCategoria.Todas;
            _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(_categoriaVMs);

            // Act
            var result = _categoriaController.GetByTipoCategoria(idUsuario, tipoCategoria) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoriaVM>>(result.Value);
            var returnedResults = result.Value as List<CategoriaVM>;
            Assert.Equal(_categoriaVMs.Count, returnedResults.Count);
        }

        [Fact]
        public void Post_ReturnsOkResult()
        {
            // Arrange
            var categoriaVM = _categoriaVMs.First();
            _mockCategoriaBusiness.Setup(b => b.Create(categoriaVM)).Returns(categoriaVM);

            // Act
            var result = _categoriaController.Post(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoriaVM>(result.Value);
            Assert.Equal(categoriaVM, result.Value);
        }

        [Fact]
        public void Post_ReturnsBadRequest()
        {
            // Arrange
            var categoriaVM = _categoriaVMs.First();
            // Assume a mismatch in the user ID
            _mockCategoriaBusiness.Setup(b => b.Create(categoriaVM)).Returns((CategoriaVM)null);

            // Act
            var result = _categoriaController.Post(categoriaVM) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Put_ReturnsOkResult()
        {
            // Arrange
            var categoriaVM = _categoriaVMs.First();
            _mockCategoriaBusiness.Setup(b => b.Update(categoriaVM)).Returns(categoriaVM);

            // Act
            var result = _categoriaController.Put(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoriaVM>(result.Value);
            Assert.Equal(categoriaVM, result.Value);
        }

        [Fact]
        public void Put_ReturnsBadRequest()
        {
            // Arrange
            var categoriaVM = _categoriaVMs.First();
            // Assume a mismatch in the user ID
            _mockCategoriaBusiness.Setup(b => b.Update(categoriaVM)).Returns((CategoriaVM)null);

            // Act
            var result = _categoriaController.Put(categoriaVM) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_ReturnsOkResult()
        {
            // Arrange
            var categoriaVM = _categoriaVMs.First();
            _mockCategoriaBusiness.Setup(b => b.Delete(categoriaVM)).Returns(true);

            // Act
            var result = _categoriaController.Delete(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(true, result.Value);
        }

        [Fact]
        public void Delete_ReturnsBadRequest()
        {
            // Arrange
            var categoriaVM = _categoriaVMs.First();
            _mockCategoriaBusiness.Setup(b => b.Delete(categoriaVM)).Returns(false);

            // Act
            var result = _categoriaController.Delete(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(false, result.Value);
        }
    }
}
