using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Despesas.WebApi.CommonDependenceInject;
public static class SwaggerApiVersioningDependenceInject
{
    private readonly static string appName = "API Despesas Pessoais RESTful com HATEOAS ";
    private readonly static string currentVersion = "v2";
    private readonly static string appDescription = @$"
       Este serviço é uma Web API RESTful com suporte a HATEOAS, desenvolvida utilizando .NET Core. O serviço é totalmente dockerizado e utiliza Entity Framework com Migrations para gerenciar o banco de dados.
       Implementamos um pipeline de CI/CD para garantir a integração contínua e a entrega contínua, proporcionando um desenvolvimento ágil e eficiente. O serviço está implantado em ambientes de desenvolvimento (DEV) e produção (PROD), hospedados em instâncias EC2 da AWS.
       Além disso, temos build,testes unitários e teste de cobertura de código estático ""SONAR CLOUD"" rodando estrategicamente a cada push ou pull request realizadas em branchs do git hub. 
       Também contamos com um serviço de testes de ponta a ponta (E2E) desenvolvido com Python e Playwright. Esse serviço de testes é executado automaticamente através de triggers configuradas no GitHub Actions, assegurando que a qualidade e a funcionalidade do aplicativo sejam verificadas em cada alteração de código.";

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
                    Name = "Projeto Web API Despesas Pessoais - Alex Ribeiro de Faria",
                    Url = new Uri("https://github.com/alexfariakof/despesas-backend-api-net-core")
                },
            });

            c.SwaggerDoc(currentVersion, new OpenApiInfo                {
                Title = appName,
                Version = currentVersion,
                Description = appDescription,
                Contact = new OpenApiContact                    {
                    Name = "Projeto Web API Despesas Pessoais - Alex Ribeiro de Faria",
                    Url = new Uri("https://github.com/alexfariakof/despesas-backend-api-net-core")
                }
            });
            
            // Filtrar os endpoints com base nos namespaces
            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                var controllerNamespace = methodInfo?.DeclaringType?.Namespace;

                if (docName == currentVersion)
                    return controllerNamespace != null && controllerNamespace.Contains("Controllers.v2");
            
                if (docName == "v1")
                    return controllerNamespace != null && controllerNamespace.Contains("Controllers.v1");

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
            c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/{currentVersion}/swagger.json", $"{currentVersion} {appName} ");
            c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", $"v1 API Despesas Pessoais");            
        });
    }
}