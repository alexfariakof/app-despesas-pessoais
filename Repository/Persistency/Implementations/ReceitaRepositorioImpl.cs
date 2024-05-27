using Domain.Entities;
using Repository.Persistency.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Repository.Persistency.Implementations;
public class ReceitaRepositorioImpl : BaseRepository<Receita>, IRepositorio<Receita>
{
    private readonly RegisterContext _context;    
    public ReceitaRepositorioImpl(RegisterContext context) : base(context)
    {
        _context = context;
    }

    public override Receita? Get(int id)
    {
        return _context.Receita.Include(d => d.Categoria).Include(d => d.Usuario).FirstOrDefault(d => d.Id.Equals(id));
    }

    public override List<Receita> GetAll()
    {
        return _context.Receita.Include(d => d.Categoria).Include(d => d.Usuario).ToList();
    }

    public override void Insert(ref Receita entity)
    {
        var categoriaId = entity.CategoriaId;
        entity.Categoria =  _context.Set<Categoria>().First(c => c.Id.Equals(categoriaId));
        _context.Add(entity);
        _context.SaveChanges();
    }

    public override void Update(ref Receita entity)
    {
        var categoriaId = entity.CategoriaId;
        entity.Categoria = _context.Set<Categoria>().First(c => c.Id.Equals(categoriaId));
        var existingEntity = _context.Receita.Find(entity.Id);
        this?._context?.Entry(existingEntity).CurrentValues.SetValues(entity);
        _context.SaveChanges();
        entity = existingEntity;
    }
}