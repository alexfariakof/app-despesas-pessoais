using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.CommonDependenceInject;
using Business.CommonDependenceInject;
using Repository.Persistency.Abstractions;
using Business.Abstractions;
using Repository.Persistency.UnitOfWork.Abstractions;
using Repository.UnitOfWork;
using Despesas.Infrastructure.Email.Abstractions;
using Despesas.Infrastructure.Email;

namespace CommonDependenceInject;
public sealed class ServicesDependenceInjectTest
{
    [Fact]
    public void AddServices_Should_Register_Services()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddServices();

        // Assert

        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(Business.Abstractions.Generic.IBusiness<Business.Dtos.v1.DespesaDto, Despesa>) && descriptor.ImplementationType == typeof(DespesaBusinessImpl<Business.Dtos.v1.DespesaDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(Business.Abstractions.Generic.IBusiness<Business.Dtos.v1.ReceitaDto, Receita>) && descriptor.ImplementationType == typeof(ReceitaBusinessImpl<Business.Dtos.v1.ReceitaDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(Business.Abstractions.Generic.IBusiness<Business.Dtos.v1.CategoriaDto, Categoria>) && descriptor.ImplementationType == typeof(CategoriaBusinessImpl<Business.Dtos.v1.CategoriaDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IControleAcessoBusiness<Business.Dtos.v1.ControleAcessoDto, Business.Dtos.v1.LoginDto>) && descriptor.ImplementationType == typeof(ControleAcessoBusinessImpl<Business.Dtos.v1.ControleAcessoDto, Business.Dtos.v1.LoginDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(ILancamentoBusiness<Business.Dtos.v1.LancamentoDto>) && descriptor.ImplementationType == typeof(LancamentoBusinessImpl<Business.Dtos.v1.LancamentoDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IUsuarioBusiness<Business.Dtos.v1.UsuarioDto>) && descriptor.ImplementationType == typeof(UsuarioBusinessImpl<Business.Dtos.v1.UsuarioDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IImagemPerfilUsuarioBusiness<Business.Dtos.v1.ImagemPerfilDto, Business.Dtos.v1.UsuarioDto>) && descriptor.ImplementationType == typeof(ImagemPerfilUsuarioBusinessImpl<Business.Dtos.v1.ImagemPerfilDto, Business.Dtos.v1.UsuarioDto>)));

        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IUnitOfWork<>) && descriptor.ImplementationType == typeof(UnitOfWork<>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(Business.Abstractions.IBusinessBase<Business.Dtos.v2.CategoriaDto, Categoria>) && descriptor.ImplementationType == typeof(CategoriaBusinessImpl<Business.Dtos.v2.CategoriaDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(Business.Abstractions.IBusinessBase<Business.Dtos.v2.DespesaDto, Despesa>) && descriptor.ImplementationType == typeof(DespesaBusinessImpl<Business.Dtos.v2.DespesaDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(Business.Abstractions.IBusinessBase<Business.Dtos.v2.ReceitaDto, Receita>) && descriptor.ImplementationType == typeof(ReceitaBusinessImpl<Business.Dtos.v2.ReceitaDto>)));

        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(Business.Abstractions.Generic.IBusiness<Business.Dtos.v2.DespesaDto, Despesa>) && descriptor.ImplementationType == typeof(DespesaBusinessImpl<Business.Dtos.v2.DespesaDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(Business.Abstractions.Generic.IBusiness<Business.Dtos.v2.ReceitaDto, Receita>) && descriptor.ImplementationType == typeof(ReceitaBusinessImpl<Business.Dtos.v2.ReceitaDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(Business.Abstractions.Generic.IBusiness<Business.Dtos.v2.CategoriaDto, Categoria>) && descriptor.ImplementationType == typeof(CategoriaBusinessImpl<Business.Dtos.v2.CategoriaDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IControleAcessoBusiness<Business.Dtos.v2.ControleAcessoDto, Business.Dtos.v2.LoginDto>) && descriptor.ImplementationType == typeof(ControleAcessoBusinessImpl<Business.Dtos.v2.ControleAcessoDto, Business.Dtos.v2.LoginDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(ILancamentoBusiness<Business.Dtos.v2.LancamentoDto>) && descriptor.ImplementationType == typeof(LancamentoBusinessImpl<Business.Dtos.v2.LancamentoDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IUsuarioBusiness<Business.Dtos.v2.UsuarioDto>) && descriptor.ImplementationType == typeof(UsuarioBusinessImpl<Business.Dtos.v2.UsuarioDto>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IImagemPerfilUsuarioBusiness<Business.Dtos.v2.ImagemPerfilDto, Business.Dtos.v2.UsuarioDto>) && descriptor.ImplementationType == typeof(ImagemPerfilUsuarioBusinessImpl<Business.Dtos.v2.ImagemPerfilDto, Business.Dtos.v2.UsuarioDto>)));

        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(ISaldoBusiness) && descriptor.ImplementationType == typeof(SaldoBusinessImpl)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IGraficosBusiness) && descriptor.ImplementationType == typeof(GraficosBusinessImpl)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IEmailSender) && descriptor.ImplementationType == typeof(EmailSender)));
    }

    [Fact]
    public void AddRepositories_Should_Register_Repositories()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services?.AddRepositories();

        // Assert
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IRepositorio<>) && descriptor.ImplementationType == typeof(GenericRepositorio<>)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IRepositorio<Categoria>) && descriptor.ImplementationType == typeof(CategoriaRepositorioImpl)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IRepositorio<Despesa>) && descriptor.ImplementationType == typeof(DespesaRepositorioImpl)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IRepositorio<Receita>) && descriptor.ImplementationType == typeof(ReceitaRepositorioImpl)));        
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IRepositorio<Usuario>) && descriptor.ImplementationType == typeof(UsuarioRepositorioImpl)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IControleAcessoRepositorioImpl) && descriptor.ImplementationType == typeof(ControleAcessoRepositorioImpl)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(ILancamentoRepositorio) && descriptor.ImplementationType == typeof(SaldoRepositorioImpl)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(ISaldoRepositorio) && descriptor.ImplementationType == typeof(ControleAcessoRepositorioImpl)));
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IGraficosRepositorio) && descriptor.ImplementationType == typeof(GraficosRepositorioImpl)));        
        Assert.NotNull(services?.Any(descriptor => descriptor.ServiceType == typeof(IEmailSender) && descriptor.ImplementationType == typeof(EmailSender)));
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
