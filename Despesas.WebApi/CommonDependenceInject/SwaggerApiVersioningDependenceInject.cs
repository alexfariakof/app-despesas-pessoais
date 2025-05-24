using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Despesas.WebApi.CommonDependenceInject;

public static class SwaggerApiVersioningDependenceInject
{
    private readonly static string appName = "Labolatório Noble Lean Culture";
    private readonly static string currentVersion = "v2";
    private readonly static string appDescription = @$"
        Eficiência com Propósito. Inovação com Consciência.
        
        O projeto Noble Lean Culture é uma iniciativa que une os princípios do Lean Thinking e da Agilidade, 
        aplicando tecnologia de ponta com uma abordagem ética e centrada em valor. Com foco em entregas rápidas, 
        redução de desperdícios e colaboração contínua, o projeto promove soluções inteligentes e sustentáveis.
        
        A base tecnológica é ancorada em infraestrutura de nuvem escalável, com atenção máxima à segurança da informação, 
        confiabilidade e resiliência. Mais do que apenas entregar sistemas eficientes, buscamos gerar impacto positivo, 
        com uma gestão consciente, humana e responsável.
        
        Valores-chave:
            🔹 Lean Thinking
            🔹 Agilidade com propósito
            🔹 Cloud-first
            🔹 Segurança by design
            🔹 Colaboração e transparência
            🔹 Entrega de valor contínuo";

    public static void AddSwaggerApiVersioning(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddApiVersioning();

        services.AddSwaggerGen(c =>
        {            
            c.SwaggerDoc(currentVersion, new OpenApiInfo
            {
                Title = appName,
                Version = currentVersion,
                Description = appDescription,
                Contact = new OpenApiContact
                {
                    Name = "Projeto de Gestão de Despesas Pessoais/Noble Lean Culture",
                    Url = new Uri("https://github.com/alexfariakof/despesas-backend-api-net-core")
                }
            });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = appName,
                Version = "v1",
                Description = $@"V1 {appDescription}",
                Contact = new OpenApiContact
                {
                    Name = "Projeto Web API Despesas Pessoais - Alex Ribeiro de Faria",
                    Url = new Uri("https://github.com/alexfariakof/despesas-backend-api-net-core")
                },
            });

            // Filtrar os endpoints com base nos namespaces
            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                var controllerNamespace = methodInfo?.DeclaringType?.Namespace;

                if (docName == currentVersion)
                    return controllerNamespace != null && controllerNamespace.Contains("Controllers.v2");

                if (docName == "v1" && !controllerNamespace.Contains("Controllers.v2"))
                    return true;

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
            c.SwaggerEndpoint(@$"{swaggerJsonBasePath}/swagger/{currentVersion}/swagger.json", $"{appName} ");
        });
    }
}