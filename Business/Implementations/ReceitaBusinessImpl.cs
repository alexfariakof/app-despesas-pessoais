using AutoMapper;
using Business.Abstractions;
using Business.Generic;
using Domain.Entities;
using Repository.Persistency.Generic;
using Domain.Entities.Abstractions;
using Business.Dtos.Core;


namespace Business.Implementations;
public class ReceitaBusinessImpl<Dto> : BusinessBase<Dto, Receita>, IBusiness<Dto, Receita> where Dto : BaseReceitaDto, new()
{
    private readonly IRepositorio<Receita> _repositorio;
    private readonly IRepositorio<Categoria> _repoCategoria;
    private readonly IMapper _mapper;    
    public ReceitaBusinessImpl(IMapper mapper, IUnitOfWork<Receita> unitOfWork, IRepositorio<Receita> repositorio, IRepositorio<Categoria> repoCategoria): base (mapper, unitOfWork)
    {
        _mapper = mapper;
        _repositorio = repositorio;
        _repoCategoria = repoCategoria;        
        _repoCategoria = repoCategoria;
    }
    public override Dto Create(Dto obj)
    {
        Receita receita = _mapper.Map<Receita>(obj);
        IsCategoriaValid(receita);
        receita.Categoria = _repoCategoria.Get(receita.CategoriaId);
        _repositorio.Insert(ref receita);
        return _mapper.Map<Dto>(receita);
    }

    public override List<Dto> FindAll(int idUsuario)
    {
        var receitas = _repositorio.GetAll().FindAll(d => d.UsuarioId == idUsuario);
        foreach (var receita in receitas)
            receita.Categoria = _repoCategoria.Get(receita.CategoriaId);
        return _mapper.Map<List<Dto>>(receitas);
    }      

    public override Dto FindById(int id, int idUsuario)
    {
        var receita = _repositorio.Get(id);
        receita.Categoria = _repoCategoria.Get(receita.CategoriaId);
        var Dto = _mapper.Map<Dto>(receita);
        return Dto;
    }

    public override Dto Update(Dto obj)
    {
        Receita receita = _mapper.Map<Receita>(obj);
        IsCategoriaValid(receita);
        receita.Categoria = _repoCategoria.Get(receita.CategoriaId);
        _repositorio.Update(ref receita);
        return _mapper.Map<Dto>(receita);
    }

    public override bool Delete(Dto obj)
    {
        Receita receita = _mapper.Map<Receita>(obj);
        return  _repositorio.Delete(receita);
    }

    private void IsCategoriaValid(Receita obj)
    {
        if (_repoCategoria.GetAll().Find(c => c.UsuarioId == obj.UsuarioId && c.TipoCategoria.Equals(TipoCategoria.Receita) && c.Id == obj.CategoriaId) == null)
            throw new ArgumentException("Categoria inválida para este usuário!");
    }
}
