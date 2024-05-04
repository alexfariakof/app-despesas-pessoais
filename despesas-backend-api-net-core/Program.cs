using despesas_backend_api_net_core.CommonDependenceInject;
using Business.CommonDependenceInject;
using Repository;
using Repository.CommonDependenceInject;
using Microsoft.EntityFrameworkCore;
using CrossCutting.CommonDependenceInject;

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

if (builder.Environment.IsProduction() || builder.Environment.EnvironmentName.Equals("MySqlServer"))
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString")));
}
else 
{
    builder.Services.CreateDataBaseInMemory();
}

// Add CommonInjectDependences 
builder.Services.AddDataSeeders();
builder.Services.ConfigureAutorization(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCrossCuttingConfiguration();
builder.Services.AddHyperMediaHATEOAS();

var app = builder.Build();
app.AddSupporteCulturesPtBr();

// Configure the HTTP request pipeline.
app.AddSwaggerApiVersioning();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute("DefaultApi", "{version=apiVersion}/{controller=values}/{id?}");
app.RunDataSeeders();
app.Run();