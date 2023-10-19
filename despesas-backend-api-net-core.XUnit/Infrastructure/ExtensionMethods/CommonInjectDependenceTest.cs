
using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using despesas_backend_api_net_core.Infrastructure.Security;
using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Infrastructure.Security.Implementation;
using despesas_backend_api_net_core.Database_In_Memory;

namespace Test.XUnit.Infrastructure.ExtensionMethods
{
    public class CommonInjectDependenceTest
    {
<<<<<<< Updated upstream
        
=======
        [Fact]
        public void AddServices_Should_Register_Services()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddServices();

            // Assert
            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IBusiness<DespesaVM>) &&
                descriptor.ImplementationType == typeof(DespesaBusinessImpl)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IBusiness<ReceitaVM>) &&
                descriptor.ImplementationType == typeof(ReceitaBusinessImpl)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IBusiness<CategoriaVM>) &&
                descriptor.ImplementationType == typeof(CategoriaBusinessImpl)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IControleAcessoBusiness) &&
                descriptor.ImplementationType == typeof(ControleAcessoBusinessImpl)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(ILancamentoBusiness) &&
                descriptor.ImplementationType == typeof(LancamentoBusinessImpl)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IUsuarioBusiness) &&
                descriptor.ImplementationType == typeof(UsuarioBusinessImpl)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IImagemPerfilUsuarioBusiness) &&
                descriptor.ImplementationType == typeof(ImagemPerfilUsuarioBusinessImpl)
            ));
        }

        [Fact]
        public void AddRepositories_Should_Register_Repositories()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddRepositories();

            // Assert
            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IRepositorio<DespesaVM>) &&
                descriptor.ImplementationType == typeof(GenericRepositorio<DespesaVM>)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IRepositorio<ReceitaVM>) &&
                descriptor.ImplementationType == typeof(GenericRepositorio<ReceitaVM>)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IRepositorio<CategoriaVM>) &&
                descriptor.ImplementationType == typeof(GenericRepositorio<CategoriaVM>)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IRepositorio<Usuario>) &&
                descriptor.ImplementationType == typeof(UsuarioRepositorioImpl)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IControleAcessoRepositorio) &&
                descriptor.ImplementationType == typeof(ControleAcessoRepositorioImpl)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(IEmailSender) &&
                descriptor.ImplementationType == typeof(EmailSender)
            ));

            Assert.NotNull(services.Any(descriptor =>
                descriptor.ServiceType == typeof(ILancamentoRepositorio) &&
                descriptor.ImplementationType == typeof(LancamentoRepositorioImpl)
            ));
        }

        [Fact]
        public void CreateDataBaseInMemory_Should_Add_Context_And_DataSeeder()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.CreateDataBaseInMemory();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetService<RegisterContext>();
            var dataSeeder = serviceProvider.GetService<IDataSeeder>();

            Assert.NotNull(context);
            Assert.NotNull(dataSeeder);
        }
>>>>>>> Stashed changes
    }
}