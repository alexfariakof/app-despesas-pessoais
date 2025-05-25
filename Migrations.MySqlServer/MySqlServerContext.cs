using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Migrations.MySqlServer;
public class MySqlServerContext : BaseContext<MySqlServerContext>
{
    public MySqlServerContext(DbContextOptions<MySqlServerContext> options) : base(options) { }
}
