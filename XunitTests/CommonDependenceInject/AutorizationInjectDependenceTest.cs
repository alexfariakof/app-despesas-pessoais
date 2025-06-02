using Business.Authentication;
using Despesas.WebApi.CommonDependenceInject;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CommonDependenceInject;
public sealed class AuthorizationInjectDependenceTest
{
    [Fact]
    public void AddAuthConfigurations_ShouldAddAuthenticationAndAuthorizationConfigurations()
    {
        // Arrange
        
        var builder = WebApplication.CreateBuilder();
        builder.Configuration.SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json");
               
        var services = builder.Services;

        // Act        
        builder.Services.AddAutoAuthenticationConfigurations();
        builder.AddSigningConfigurations();
        var configuration = builder.Build();

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<SigningConfigurations>());

        // Assert authentication configurations
        var authenticationOptions = serviceProvider.GetRequiredService<IOptions<AuthenticationOptions>>().Value;
        Assert.Equal(JwtBearerDefaults.AuthenticationScheme, authenticationOptions.DefaultAuthenticateScheme);
        Assert.Equal(JwtBearerDefaults.AuthenticationScheme, authenticationOptions.DefaultChallengeScheme);

        // Assert JWT bearer configurations
        var jwtBearerOptions = serviceProvider.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>().Get(JwtBearerDefaults.AuthenticationScheme);
        var signingConfigurations = serviceProvider.GetRequiredService<SigningConfigurations>();

        Assert.NotNull(jwtBearerOptions);
        Assert.NotNull(jwtBearerOptions.TokenValidationParameters);
        Assert.Equal(signingConfigurations.Key, jwtBearerOptions.TokenValidationParameters.IssuerSigningKey);
        Assert.True(jwtBearerOptions.TokenValidationParameters.ValidateIssuerSigningKey);
        Assert.True(jwtBearerOptions.TokenValidationParameters.ValidateLifetime);
        Assert.Equal(TimeSpan.Zero, jwtBearerOptions.TokenValidationParameters.ClockSkew);

        // Assert authorization policy
        var authorizationPolicy = serviceProvider.GetRequiredService<IAuthorizationPolicyProvider>().GetPolicyAsync("Bearer")?.Result;
        Assert.NotNull(authorizationPolicy);
        Assert.Contains(JwtBearerDefaults.AuthenticationScheme, authorizationPolicy.AuthenticationSchemes);
    }
}