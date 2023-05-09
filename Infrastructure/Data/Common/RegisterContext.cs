using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using Microsoft.EntityFrameworkCore;

namespace despesas_backend_api_net_core.Infrastructure.Data.Common
{
    public class RegisterContext : DbContext
    {
        public RegisterContext(DbContextOptions<RegisterContext> options)
            : base(options)
        {

        }

        public DbSet<ControleAcesso> ControleAcessos { get; set; }
        public DbSet<Usuario> Usuarios{ get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Categoria> Categorias{ get; set; }
        public DbSet<Lancamento> Lancamentos{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoriaMap());
        }
    }
}
