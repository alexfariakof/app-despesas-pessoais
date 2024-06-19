using Domain.Entities;
using Repository.Persistency.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Repository.Persistency.Implementations;
public class ReceitaRepositorioImpl : BaseRepository<Receita>, IRepositorio<Receita>
{
    public RegisterContext Context { get; }
    public ReceitaRepositorioImpl(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override Receita Get(int id)
    {
        return Context.Receita.Include(d => d.Categoria).Include(d => d.Usuario).FirstOrDefault(d => d.Id.Equals(id));
    }

    public override List<Receita> GetAll()
    {
        return Context.Receita.Include(d => d.Categoria).Include(d => d.Usuario).ToList();
    }

    public override void Insert(ref Receita entity)
    {
        var categoriaId = entity.CategoriaId;
        entity.Categoria =  Context.Set<Categoria>().First(c => c.Id.Equals(categoriaId));
        Context.Add(entity);
        Context.SaveChanges();
    }

    public override void Update(ref Receita entity)
    {
        var receitaId = entity.Id;
        var categoriaId = entity.CategoriaId;
        entity.Categoria = Context.Set<Categoria>().First(c => c.Id.Equals(categoriaId));
        var existingEntity = Context.Receita.Single(prop => prop.Id.Equals(receitaId));
        Context?.Entry(existingEntity).CurrentValues.SetValues(entity);
        Context?.SaveChanges();
        entity = existingEntity;
    }
}