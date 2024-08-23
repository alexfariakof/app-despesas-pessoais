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
builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();

    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning();
builder.Services.AddSwaggerApiVersioning();

if (builder.Environment.EnvironmentName.Equals("MySqlServer"))
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString") ?? throw new NullReferenceException("MySqlConnectionString not defined.")));
}
else if (builder.Environment.IsProduction() || builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    builder.Services.CreateDataBaseInMemory();
}
else 
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString")));
    builder.Services.ConfigureMsSqlServerMigrationsContext(builder.Configuration);
    builder.Services.ConfigureMySqlServerMigrationsContext(builder.Configuration);
}

//Add SigningConfigurations
builder.Services.AddSigningConfigurations(builder.Configuration);

// Add AutoAuthConfigurations
builder.Services.AddAutoAuthenticationConfigurations();

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
if (app.Environment.IsProduction())
    app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();
app.AddSupporteCulturesPtBr();
app.UseCors();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{version=apiVersion}/{controller=values}/{id?}");

if (!app.Environment.IsProduction())
    app.AddSwaggerUIApiVersioning();
 
app.UseAuthentication();
app.UseRouting()
    .UseAuthorization()
    .UseCertificateForwarding()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("index.html");
    });

//if (!app.Environment.IsProduction())
    app.RunDataSeeders();

app.Run();