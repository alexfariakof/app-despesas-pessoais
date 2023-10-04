using despesas_backend_api_net_core.Infrastructure.Data.Common;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using despesas_backend_api_net_core.Infrastructure.Security.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add Cors Configuration 
builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();

    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v4",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "API Despesas Pessoais V4",
            Version = "4.0.7"
        });
});

builder.Services.AddDbContext<RegisterContext>(c => c.UseInMemoryDatabase("Register"));

ConfigureAutorization(builder.Services, builder.Configuration);
ConfigureCrypto(builder.Services, builder.Configuration);
ConfigureAmazonS3Access(builder.Services, builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v4/swagger.json", "API Despesas Pessoais V4");
});
//}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();


app.MapControllers();
app.Run();

static void ConfigureAutorization(IServiceCollection services, IConfiguration configuration)
{
    SigningConfigurations signingConfigurations = new SigningConfigurations();
    services.AddSingleton(signingConfigurations);

    TokenConfiguration tokenConfigurations = new TokenConfiguration();

    new ConfigureFromConfigurationOptions<TokenConfiguration>(
        configuration.GetSection("TokenConfigurations")
    )
    .Configure(tokenConfigurations);

    services.AddSingleton(tokenConfigurations);


    services.AddAuthentication(authOptions =>
    {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(bearerOptions =>
    {
        Microsoft.IdentityModel.Tokens.TokenValidationParameters paramsValidation = bearerOptions.TokenValidationParameters;
        paramsValidation.IssuerSigningKey = signingConfigurations.Key;
        paramsValidation.ValidAudience = tokenConfigurations.Audience;
        paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

        // Validates the signing of a received token
        paramsValidation.ValidateIssuerSigningKey = true;

        // Checks if a received token is still valid
        paramsValidation.ValidateLifetime = true;

        // Tolerance time for the expiration of a token (used in case
        // of time synchronization problems between different
        // computers involved in the communication process)
        paramsValidation.ClockSkew = TimeSpan.Zero;
    });

    // Enables the use of the token as a means of
    // authorizing access to this project's resources
    services.AddAuthorization(auth =>
    {
        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
            .RequireAuthenticatedUser().Build());
    });
}
static void ConfigureCrypto(IServiceCollection services, IConfiguration configuration)
{
    Crypto.GetInstance.SetCryptoKey(configuration.GetSection("Crypto:Key").Value);
}
static void ConfigureAmazonS3Access(IServiceCollection services, IConfiguration configuration)
{
    var accessKey = configuration.GetSection("AmazonS3Bucket:accessKey").Value;
    var secretAccessKey = configuration.GetSection("AmazonS3Bucket:secretAccessKey").Value;
    var s3ServiceUrl = configuration.GetSection("AmazonS3Bucket:s3ServiceUrl").Value;
    var bucketName = configuration.GetSection("AmazonS3Bucket:bucketName").Value;
    AmazonS3Bucket.GetInstance.SetConfiguration(accessKey, secretAccessKey, s3ServiceUrl, bucketName);
}
