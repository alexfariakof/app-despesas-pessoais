using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Despesas.WebApi.CommonDependenceInject;
public static class SwaggerApiVersioningDependenceInject
{
    private readonly static string appName = "API Restful with DDD/UnitOfWork/CQRS/HATEOAS";
    private readonly static string currentVersion = "v2";
    private readonly static string appDescription = $"API Restful HATEOAS .Net Core 8.0 com CI/CD, Dockerizada utilizando Entity Framework  e Migrations.";

    public static void AddSwaggerApiVersioning(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = appName,
                Version = "v1",
                Description = appDescription,
                Contact = new OpenApiContact
                {
                    Name = "Alex Ribeiro de Faria",
                    Url = new Uri("https://github.com/alexfariakof/despesas-backend-api-net-core")
                },
            });

            c.SwaggerDoc(currentVersion, new OpenApiInfo                {
                Title = appName,
                Version = currentVersion,
                Description = appDescription,
                Contact = new OpenApiContact                    {
                    Name = "Alex Ribeiro de Faria",
                    Url = new Uri("https://github.com/alexfariakof/despesas-backend-api-net-core")
                }
            });
            
            // Filtrar os endpoints com base nos namespaces
            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                var controllerNamespace = methodInfo?.DeclaringType?.Namespace;

                if (docName == "v1")
                    return controllerNamespace != null && controllerNamespace.Contains("Controllers.v1");
                if (docName == currentVersion)
                    return controllerNamespace != null && controllerNamespace.Contains("Controllers.v2");

                return false;
            });

        });

    }

    public static void AddSwaggerUIApiVersioning(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
            c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", $"v1 API Despesas Pessoais .net core 8.0");
            c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/{currentVersion}/swagger.json", $"{currentVersion} {appName} ");
        });
    }
}