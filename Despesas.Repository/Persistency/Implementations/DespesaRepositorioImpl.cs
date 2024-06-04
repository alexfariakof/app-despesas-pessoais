using Domain.Entities;
using Repository.Persistency.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Repository.Persistency.Implementations;
public class DespesaRepositorioImpl : BaseRepository<Despesa>, IRepositorio<Despesa>
{
    public RegisterContext Context { get; }
    public DespesaRepositorioImpl(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override Despesa? Get(int id)
    {
        return Context.Despesa.Include(d => d.Categoria).Include(d => d.Usuario).FirstOrDefault(d => d.Id.Equals(id));
    }

    public override List<Despesa> GetAll()
    {
        return Context.Despesa.Include(d => d.Categoria).Include(d => d.Usuario).ToList();
    }

    public override void Insert(ref Despesa entity)
    {
        var categoriaId = entity.CategoriaId;
        entity.Categoria = Context.Set<Categoria>().First(c => c.Id.Equals(categoriaId));
        Context.Add(entity);
        Context.SaveChanges();
    }

    public override void Update(ref Despesa entity)
    {
        var despesaId = entity.Id;
        var categoriaId = entity.CategoriaId;
        entity.Categoria = Context.Set<Categoria>().First(c => c.Id.Equals(categoriaId));
        var existingEntity = Context.Despesa.Single(d => d.Id.Equals(despesaId));
        Context?.Entry(existingEntity).CurrentValues.SetValues(entity);        
        Context?.SaveChanges();
        entity = existingEntity;
    }
}