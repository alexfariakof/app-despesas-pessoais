using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Implementations;

namespace despesas_backend_api_net_core.Infrastructure.ExtensionMethods
{
    public static class CommonInjectDependence
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBusiness<UsuarioVM>), typeof(UsuarioBusinessImpl));
            services.AddScoped(typeof(IBusiness<ImagemPerfilUsuarioVM>), typeof(ImagemPerfilUsuarioBusinessImpl));
            services.AddScoped(typeof(IBusiness<DespesaVM>), typeof(DespesaBusinessImpl));
            services.AddScoped(typeof(IBusiness<ReceitaVM>), typeof(ReceitaBusinessImpl));
            services.AddScoped(typeof(IBusiness<CategoriaVM>), typeof(CategoriaBusinessImpl));
            services.AddScoped(typeof(IControleAcessoBusiness), typeof(ControleAcessoBusinessImpl));
            services.AddScoped(typeof(ILancamentoBusiness), typeof(LancamentoBusinessImpl));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositorio<>), typeof(GenericRepositorio<>));
            services.AddScoped(typeof(IRepositorio<Usuario>), typeof(UsuarioRepositorioImpl));
            services.AddScoped(typeof(IControleAcessoRepositorio), typeof(ControleAcessoRepositorioImpl));
            services.AddScoped(typeof(ILancamentoRepositorio), typeof(LancamentoRepositorioImpl));

            return services;
        }
    }
}