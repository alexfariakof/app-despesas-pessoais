using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Repository;
public class RegisterContext : BaseContext<RegisterContext>
{
    public RegisterContext(DbContextOptions<RegisterContext> options) : base(options) { }
}
