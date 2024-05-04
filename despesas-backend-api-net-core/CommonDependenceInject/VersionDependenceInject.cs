using Microsoft.OpenApi.Models;

namespace despesas_backend_api_net_core.CommonDependenceInject;
public static class VersionDependenceInject
{
    public readonly static string appName = "Serviços de Streaming";
    public readonly static string appVersion = "v6";
    public readonly static string appDescription = $"API Serviços de Streaming.";

    public static void AddApiVersioning(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => {
            c.SwaggerDoc(appVersion,
            new OpenApiInfo
            {
                Title = appName,
                Version = appVersion,
                Description = appDescription,
                Contact = new OpenApiContact
                {
                    Name = "Alex Ribeiro de Faria",
                    Url = new Uri("https://github.com/alexfariakof/despesas-backend-api-net-core")
                }
            });
        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v6",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API Version 6",
                    Version = "6.0.2"
                });
        });


    }
}