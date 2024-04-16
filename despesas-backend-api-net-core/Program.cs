using despesas_backend_api_net_core.CommonDependenceInject;
using Business.CommonDependenceInject;
using Repository;
using Repository.CommonDependenceInject;
using DataSeeders;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v6",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "API Version 6",
            Version = "6.0.0"
        });
});

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString")));
}
else 
{
    builder.Services.CreateDataBaseInMemory();
}

// Add CommonInjectDependences 
builder.Services.ConfigureAutorization(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCrossCuttingConfiguration();


var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("pt-BR") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v6/swagger.json", "API Version 6 with DDD/CQRS");
});

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();

// Not existis folder  wwwroot for SPA projects
//app.UseStaticFiles();
app.MapControllers();

if (!app.Environment.IsProduction())
{ 
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dataSeeder = services.GetRequiredService<IDataSeeder>();
        dataSeeder.SeedData();
    }
}

app.Run();