using Microsoft.EntityFrameworkCore;
using Repository.CommonDependenceInject;
using DataSeeders;
using DataSeeders.Implementations;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString"),
b => b.MigrationsAssembly("Migrations.MySqlServer")));

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