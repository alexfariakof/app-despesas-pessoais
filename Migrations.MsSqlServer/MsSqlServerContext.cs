using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Migrations.MsSqlServer;
public class MsSqlServerContext : BaseContext<MsSqlServerContext>
{
    public MsSqlServerContext(DbContextOptions<MsSqlServerContext> options) : base(options) { }
}
