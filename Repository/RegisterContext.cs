using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Mapping;

namespace Repository;
public class RegisterContext : DbContext
{
    public RegisterContext(DbContextOptions<RegisterContext> options): base(options)  { }
    public DbSet<ControleAcesso> ControleAcesso { get; set; }
    public DbSet<Usuario> Usuario{ get; set; }
    public DbSet<ImagemPerfilUsuario> ImagemPerfilUsuario { get; set; }
    public DbSet<Despesa> Despesa { get; set; }
    public DbSet<Receita> Receita { get; set; }
    public DbSet<Categoria> Categoria{ get; set; }
    public DbSet<Lancamento> Lancamento{ get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CategoriaMap());
        modelBuilder.ApplyConfiguration(new UsuarioMap());
        modelBuilder.ApplyConfiguration(new ImagemPerfilUsuarioMap());
        modelBuilder.ApplyConfiguration(new ControleAcessoMap());
        modelBuilder.ApplyConfiguration(new DespesaMap());
        modelBuilder.ApplyConfiguration(new ReceitaMap());
        modelBuilder.ApplyConfiguration(new LancamentoMap());
    }
}