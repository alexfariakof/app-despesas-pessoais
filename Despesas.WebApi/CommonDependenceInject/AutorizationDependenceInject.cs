using Business.Authentication;
using Despesas.Business.Authentication.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace Despesas.WebApi.CommonDependenceInject;
public static class AutorizationDependenceInject
{
    public static void AddSigningConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenConfigurations"));
        var options = builder.Services.BuildServiceProvider().GetService<IOptions<TokenOptions>>();
        string certificatePath = Path.Combine(AppContext.BaseDirectory, options.Value.Certificate);
        X509Certificate2 certificate = new X509Certificate2(certificatePath, options.Value.Password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
        var signingConfigurations = new SigningConfigurations(certificate, options);
        builder.Services.AddSingleton<SigningConfigurations>(signingConfigurations);

        if (builder.Environment.IsProduction())
        {
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ConfigureHttpsDefaults(httpsOptions =>
                {
                    httpsOptions.ServerCertificate = certificate;
                });
            });
        }

    }

    public static void AddAutoAuthenticationConfigurations(this IServiceCollection services)
    {
        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
            var options = services.BuildServiceProvider().GetService<IOptions<TokenOptions>>();

            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = services.BuildServiceProvider().GetService<SigningConfigurations>().Key,
                ValidAudience =  options.Value.Audience,
                ValidIssuer = options.Value.Issuer,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​).RequireAuthenticatedUser().Build());
        });
    }
}