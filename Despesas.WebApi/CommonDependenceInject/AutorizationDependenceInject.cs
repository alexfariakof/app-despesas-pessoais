using Business.Authentication;
using Business.Authentication.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Despesas.WebApi.CommonDependenceInject;
public static class AutorizationDependenceInject
{
    public static void AddSigningConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOptions>(configuration.GetSection("TokenConfigurations"));
        var options = services.BuildServiceProvider().GetService<IOptions<TokenOptions>>();
        var signingConfigurations = new SigningConfigurations(options);
        services.AddSingleton<SigningConfigurations>(signingConfigurations);
    }

    public static void AddAutoAuthenticationConfigurations(this IServiceCollection services)
    {
        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
            var configurations = services.BuildServiceProvider().GetService<SigningConfigurations>().TokenConfiguration;
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = services.BuildServiceProvider().GetService<SigningConfigurations>().Key,
                ValidAudience = configurations.Audience,
                ValidIssuer = configurations.Issuer,
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