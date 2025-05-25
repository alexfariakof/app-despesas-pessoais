using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Despesas.WebApi.CommonDependenceInject;
using Business.Authentication.Abstractions;
using Business.Authentication;

namespace CommonDependenceInject;
public sealed class AuthorizationInjectDependenceTest
{
    [Fact]
    public void AddAuthConfigurations_ShouldAddAuthenticationAndAuthorizationConfigurations()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();
        var services = new ServiceCollection();

        // Act
        services.AddSigningConfigurations(configuration);
        services.AddAutoAuthenticationConfigurations();

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