using Business.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace despesas_backend_api_net_core.CommonDependenceInject;
public static class AutorizationDependenceInject
{
    public static void AddAuthConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOptions>(configuration.GetSection("TokenConfigurations"));
        services.AddSingleton<SigningConfigurations>();
        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
            var signingConfigurations = services.BuildServiceProvider().GetRequiredService<SigningConfigurations>();
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