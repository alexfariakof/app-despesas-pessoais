using Microsoft.EntityFrameworkCore;
using Repository.CommonInjectDependence;
using Repository;
using DataSeeders;
using DataSeeders.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString"),
b => b.MigrationsAssembly("MySqlServer.Migrations")));

builder.Services.AddRepositories();
builder.Services.AddTransient<IDataSeeder, DataSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dataSeeder = services.GetRequiredService<IDataSeeder>();
    dataSeeder.SeedData();
}

app.Run();