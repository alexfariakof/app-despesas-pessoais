using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace despesas_backend_api_net_core.Infrastructure.Data.Common
{
    public class RegisterContext : DbContext
    {
        public RegisterContext(DbContextOptions<RegisterContext> options)
            : base(options)
        {

        }

        public DbSet<ControleAcesso> ControleAcesso { get; set; }
        public DbSet<Usuario> Usuario{ get; set; }
        public DbSet<Despesa> Despesa { get; set; }
        public DbSet<Receita> Receita { get; set; }
        public DbSet<Categoria> Categoria{ get; set; }
        public DbSet<Lancamento> Lancamento{ get; set; }       

    }
}

