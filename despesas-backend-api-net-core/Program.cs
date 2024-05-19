using Microsoft.EntityFrameworkCore;
using despesas_backend_api_net_core.CommonDependenceInject;
using Business.CommonDependenceInject;
using Repository.CommonDependenceInject;
using CrossCutting.CommonDependenceInject;
using Migrations.MsSqlServer.CommonInjectDependence;
using Migrations.MySqlServer.CommonInjectDependence;
using Repository;

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

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning();
builder.Services.AddSwaggerApiVersioning();
                                                                                                                                                 
if (builder.Environment.IsProduction()) 
{
    builder.Services.CreateDataBaseInMemory();
}
else if (builder.Environment.EnvironmentName.Equals("Azure"))
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AzureMsSqlConnectionString")));
}    
else if (builder.Environment.EnvironmentName.Equals("MySqlServer"))
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString")));
}
else if (builder.Environment.EnvironmentName.Equals("DatabaseInMemory"))
{
    builder.Services.CreateDataBaseInMemory();
}
else 
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString")));
    builder.Services.ConfigureMsSqlServerMigrationsContext(builder.Configuration);
    builder.Services.ConfigureMySqlServerMigrationsContext(builder.Configuration);
}

// Add CommonInjectDependences 
builder.Services.AddAutoMapper();
builder.Services.AddDataSeeders();
builder.Services.AddAuthConfigurations(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCrossCuttingConfiguration();
builder.Services.AddHyperMediaHATEOAS();

var app = builder.Build();

// Configure the HTTP request pipeline
app.AddSupporteCulturesPtBr();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute("DefaultApi", "{version=apiVersion}/{controller=values}/{id?}");
<<<<<<< HEAD
=======

if (!app.Environment.IsProduction())
    app.AddSwaggerUIApiVersioning();

if (app.Environment.IsDevelopment())
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
app.RunDataSeeders();
app.Run();