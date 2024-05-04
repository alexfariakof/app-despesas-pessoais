using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace despesas_backend_api_net_core.CommonDependenceInject;
public static class SwaggerApiVersioningDependenceInject
{
    private readonly static string appName = "API Version with DDD/UnitOfWork/CQRS/HATEOAS";
    private readonly static string appVersion = "v1";
    private readonly static string appDescription = $"API Restful HATEOAS .Net Core 8.0 com CI/CD, Dockerizada utilizando Entity Framework  e Migrations.";

    public static void AddSwaggerApiVersioning(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
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

            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;
                var controllerNamespace = methodInfo.DeclaringType.Namespace;
                return controllerNamespace != null && controllerNamespace.Contains("Controllers.v1");
            });

            c.SwaggerDoc("v2",
                new OpenApiInfo
                {
                    Title = appName,
                    Version = "v2",
                    Description = appDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Alex Ribeiro de Faria",
                        Url = new Uri("https://github.com/alexfariakof/despesas-backend-api-net-core")
                    }
                });


            // Inclui apenas os controllers do namespace 'Controllers.v2' na documentação da versão 2
            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;
                var controllerNamespace = methodInfo.DeclaringType.Namespace;
                return controllerNamespace != null && controllerNamespace.Contains("Controllers.v2");
            });
        });
    }

    public static void AddSwaggerApiVersioning(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
            c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/{appVersion}/swagger.json", $"{appVersion} {appName} ");
            c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v2/swagger.json", $"v2 {appName}");
        });
    }
}