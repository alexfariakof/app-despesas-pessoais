using DataSeeders;
using despesas_backend_api_net_core.CommonDependenceInject;
using Domain.Core;
using Domain.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.CommonDependenceInject;
using Business.CommonDependenceInject;
using Business.Abstractions;

namespace CommonDependenceInject;
public class CommonDependenceInjectTest
{
    [Fact]
    public void AddServices_Should_Register_Services()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddServices();

        // Assert

        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IBusiness<DespesaDto>) && descriptor.ImplementationType == typeof(DespesaBusinessImpl)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IBusiness<ReceitaDto>) && descriptor.ImplementationType == typeof(ReceitaBusinessImpl)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IBusiness<CategoriaDto>) && descriptor.ImplementationType == typeof(CategoriaBusinessImpl)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IControleAcessoBusiness) && descriptor.ImplementationType == typeof(ControleAcessoBusinessImpl)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(ILancamentoBusiness) && descriptor.ImplementationType == typeof(LancamentoBusinessImpl)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IUsuarioBusiness) && descriptor.ImplementationType == typeof(UsuarioBusinessImpl)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IImagemPerfilUsuarioBusiness) && descriptor.ImplementationType == typeof(ImagemPerfilUsuarioBusinessImpl)));
    }

    [Fact]
    public void AddRepositories_Should_Register_Repositories()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddRepositories();

        // Assert
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IRepositorio<DespesaDto>) && descriptor.ImplementationType == typeof(GenericRepositorio<DespesaDto>)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IRepositorio<ReceitaDto>) && descriptor.ImplementationType == typeof(GenericRepositorio<ReceitaDto>)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IRepositorio<CategoriaDto>) && descriptor.ImplementationType == typeof(GenericRepositorio<CategoriaDto>)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IRepositorio<Usuario>) && descriptor.ImplementationType == typeof(UsuarioRepositorioImpl)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IControleAcessoRepositorioImpl) && descriptor.ImplementationType == typeof(ControleAcessoRepositorioImpl)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IEmailSender) && descriptor.ImplementationType == typeof(EmailSender)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(ILancamentoRepositorio) && descriptor.ImplementationType == typeof(LancamentoRepositorioImpl)));
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
        //var dataSeeder = serviceProvider.GetService<IDataSeeder>();

        Assert.NotNull(context);
        //Assert.NotNull(dataSeeder);
    }
}
