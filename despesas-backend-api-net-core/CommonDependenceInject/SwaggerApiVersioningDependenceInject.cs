using Microsoft.OpenApi.Models;

namespace despesas_backend_api_net_core.CommonDependenceInject;
public static class SwaggerApiVersioningDependenceInject
{
    private readonly static string appName = "API Version with DDD/UnitOfWork/CQRS/HATEOAS";
    private readonly static string appVersion = "v6";
    private readonly static string appDescription = $"API Restful HATEOAS .Net Core 8.0 com CI/CD, Dockerizada utilizando Entity Framework  e Migrations.";

    public static void AddSwaggerApiVersioning(this IServiceCollection services)
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
    }

    public static void AddSwaggerApiVersioning(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
            c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/{appVersion}/swagger.json", $"{appName} {appVersion}");
        });
    }
}