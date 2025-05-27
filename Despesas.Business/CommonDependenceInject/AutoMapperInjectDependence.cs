using Business.Dtos.Core.Profile;
using Microsoft.Extensions.DependencyInjection;

namespace Business.CommonDependenceInject;

public static class AutoMapperInjectDependence
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ControleAcessoProfile).Assembly);
        services.AddAutoMapper(typeof(CategoriaProfile).Assembly);
        services.AddAutoMapper(typeof(DespesaProfile).Assembly);
        services.AddAutoMapper(typeof(ImagemPerfilUsuarioProfile).Assembly);
        services.AddAutoMapper(typeof(LancamentoProfile).Assembly);
        services.AddAutoMapper(typeof(ReceitaProfile).Assembly);
        services.AddAutoMapper(typeof(UsuarioProfile).Assembly);
        return services;
    }
}