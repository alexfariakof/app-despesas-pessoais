using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Database_In_Memory;
using despesas_backend_api_net_core.Database_In_Memory.Implementations;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Implementations;
using despesas_backend_api_net_core.Infrastructure.Security;
using despesas_backend_api_net_core.Infrastructure.Security.Implementation;
using Microsoft.EntityFrameworkCore;


namespace despesas_backend_api_net_core.Infrastructure.ExtensionMethods
{
    public static class CommonInjectDependence
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBusiness<ImagemPerfilUsuarioVM>), typeof(ImagemPerfilUsuarioBusinessImpl));
            services.AddScoped(typeof(IBusiness<DespesaVM>), typeof(DespesaBusinessImpl));
            services.AddScoped(typeof(IBusiness<ReceitaVM>), typeof(ReceitaBusinessImpl));
            services.AddScoped(typeof(IBusiness<CategoriaVM>), typeof(CategoriaBusinessImpl));
            services.AddScoped(typeof(IControleAcessoBusiness), typeof(ControleAcessoBusinessImpl));
            services.AddScoped(typeof(ILancamentoBusiness), typeof(LancamentoBusinessImpl));
            services.AddScoped(typeof(IUsuarioBusiness), typeof(UsuarioBusinessImpl));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositorio<>), typeof(GenericRepositorio<>));
            services.AddScoped(typeof(IRepositorio<Usuario>), typeof(UsuarioRepositorioImpl));
            services.AddScoped<IControleAcessoRepositorio, ControleAcessoRepositorioImpl>();
            services.AddScoped<IEmailSender, EmailSender>();           
            //services.AddScoped(typeof(IControleAcessoRepositorio), typeof(ControleAcessoRepositorioImpl));
            services.AddScoped(typeof(ILancamentoRepositorio), typeof(LancamentoRepositorioImpl));

            return services;
        }

        public static void CreateDataBaseInMemory(this IServiceCollection services)
        {
            services.AddDbContext<RegisterContext>(c => c.UseInMemoryDatabase("Register"));
            services.AddTransient<IDataSeeder, DataSeeder>();
        }
    }
}