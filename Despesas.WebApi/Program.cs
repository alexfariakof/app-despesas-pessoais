using Business.CommonDependenceInject;
using CrossCutting.CommonDependenceInject;
using Despesas.WebApi.CommonDependenceInject;
using Microsoft.EntityFrameworkCore;
using Migrations.MsSqlServer.CommonInjectDependence;
using Migrations.MySqlServer.CommonInjectDependence;
using Repository;
using Repository.CommonDependenceInject;

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
                "https://alexfariakof.com:42535",
                "http://alexfariakof.com:42536",
                "http://localhost",
                "https://localhost",
                "http://localhost:42536",
                "https://localhost:42535",
                "https://localhost:4200",
                "http://127.0.0.1",
                "https://127.0.0.1",
                "http://127.0.0.1:42536",
                "https://127.0.0.1:42535")
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
else if (builder.Environment.IsStaging() || builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("Dev.MySqlConnectionString") ?? throw new NullReferenceException("MySqlConnectionString not defined.")));
}
else
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString") ?? throw new NullReferenceException("MySqlConnectionString not defined.")));
}

//Add SigningConfigurations
builder.AddSigningConfigurations();

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

if (builder.Environment.IsStaging())
{
    builder.WebHost.UseUrls("https://0.0.0.0:42535", "http://0.0.0.0:42536");
}

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

if (!app.Environment.IsProduction() && !app.Environment.IsStaging() && !app.Environment.IsDevelopment())
    app.RunDataSeeders();

if (app.Environment.IsStaging())
{
    app.Urls.Add("https://0.0.0.0:42535");
    app.Urls.Add("http://0.0.0.0:42536");
}

app.Run();