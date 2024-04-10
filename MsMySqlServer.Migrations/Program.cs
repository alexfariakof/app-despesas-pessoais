using Microsoft.EntityFrameworkCore;
using Repository.CommonInjectDependence;

using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RegisterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString"),
b => b.MigrationsAssembly("MsMySqlServer.Migrations")));

builder.Services.AddRepositories();


var app = builder.Build();
app.Run();