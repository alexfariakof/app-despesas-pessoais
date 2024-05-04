using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace despesas_backend_api_net_core.CommonDependenceInject;
public static class SwaggerApiVersioningDependenceInject
{
    private readonly static string appName = "API Restful with DDD/UnitOfWork/CQRS/HATEOAS";
    private readonly static string currenetVersion = "v2";
    private readonly static string appDescription = $"API Restful HATEOAS .Net Core 8.0 com CI/CD, Dockerizada utilizando Entity Framework  e Migrations.";

    public static void AddSwaggerApiVersioning(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = appName,
                Version = "v1",
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

            c.SwaggerDoc(currenetVersion,
                new OpenApiInfo
                {
                    Title = appName,
                    Version = currenetVersion,
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
                return controllerNamespace != null && controllerNamespace.Contains($"Controllers.{currenetVersion}");
            });
        });
    }

    public static void AddSwaggerApiVersioning(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
            c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", $"v1 {appName}");
            c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/{currenetVersion}/swagger.json", $"{currenetVersion} {appName} ");            
        });
    }
}