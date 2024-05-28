using Business.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Despesas.WebApi.CommonDependenceInject;
public static class AutorizationDependenceInject
{
    public static void AddAuthConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOptions>(configuration.GetSection("TokenConfigurations"));
        var options = services.BuildServiceProvider().GetService<IOptions<TokenOptions>>();
        var signingConfigurations = new SigningConfigurations(options);
        services.AddSingleton<SigningConfigurations>(signingConfigurations);
        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
            var tokenConfiguration = signingConfigurations.TokenConfiguration;
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = signingConfigurations.Key,
                ValidAudience = tokenConfiguration.Audience,
                ValidIssuer = tokenConfiguration.Issuer,
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