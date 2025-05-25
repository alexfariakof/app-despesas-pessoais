using Microsoft.EntityFrameworkCore;
using Despesas.WebApi.CommonDependenceInject;
using Business.CommonDependenceInject;
using Repository.CommonDependenceInject;
using CrossCutting.CommonDependenceInject;
using Migrations.MsSqlServer.CommonInjectDependence;
using Migrations.MySqlServer.CommonInjectDependence;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add Cors Configurations 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
                "https://alexfariakof.com",
                "http://alexfariakof.com",
                "http://alexfariakof.com:3000",
                "http://localhost",
                "https://localhost",
                "https://localhost:4200",
                "http://127.0.0.1",
                "https://127.0.0.1")
              .AllowAnyMethod()
              .AllowAnyOrigin()
              .AllowAnyHeader();
    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddSwaggerApiVersioning();

if (builder.Environment.EnvironmentName.Equals("Migrations"))
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString")));
    builder.Services.ConfigureMsSqlServerMigrationsContext(builder.Configuration);
    builder.Services.ConfigureMySqlServerMigrationsContext(builder.Configuration);
}
else if (builder.Environment.EnvironmentName.Equals("Staging"))
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("Dev.MySqlConnectionString") ?? throw new NullReferenceException("MySqlConnectionString not defined.")));
}
else
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString") ?? throw new NullReferenceException("MySqlConnectionString not defined.")));
}

//Add SigningConfigurations
builder.Services.AddSigningConfigurations(builder.Configuration);

// Add AutoAuthConfigurations
builder.Services.AddAutoAuthenticationConfigurations();

// Add Cryptography Configurations
builder.Services.AddServicesCryptography(builder.Configuration);

// Add CommonDependencesInject 
builder.Services.AddAutoMapper();
builder.Services.AddDataSeeders();
builder.Services.AddAmazonS3BucketConfigurations(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCrossCuttingConfiguration();
builder.Services.AddHyperMediaHATEOAS();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHsts();
app.UseHttpsRedirection();
app.AddSupporteCulturesPtBr();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();

//if (!app.Environment.IsProduction())
app.AddSwaggerUIApiVersioning();

app.UseAuthentication();
app.UseRouting()
    .UseAuthorization()
    .UseCertificateForwarding()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapControllerRoute(name: "DefaultApi", pattern: "v{version=apiVersion}/{controller=values}/{id?}");
        endpoints.MapFallbackToFile("index.html");
    });

if (!app.Environment.IsProduction() && ! app.Environment.EnvironmentName.Equals("Staging"))
    app.RunDataSeeders();

app.Run();