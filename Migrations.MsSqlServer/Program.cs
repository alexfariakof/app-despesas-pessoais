using DataSeeders;
using DataSeeders.Implementations;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.CommonDependenceInject;
using System.Diagnostics;

bool isDebug = Debugger.IsAttached || !Debugger.IsAttached && Debugger.IsLogging();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RegisterContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("MsSqlConnectionString"),
        b => b.MigrationsAssembly("Migrations.MsSqlServer")));

builder.Services.AddRepositories();
builder.Services.AddTransient<IDataSeeder, DataSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (isDebug && !app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dataSeeder = services.GetRequiredService<IDataSeeder>();
        dataSeeder.SeedData();
    }
}

app.MapGet("/", () => "Migrations MsSqlServer!");
app.Run();
