using AutoMapper;
using Business.Abstractions;
using Business.Dtos.Core;
using Business.Generic;
using Domain.Entities;
using Repository.Persistency.Generic;
using Domain.Entities.Abstractions;


namespace Business.Implementations;
public class ReceitaBusinessImpl : BusinessBase<BaseReceitaDto, Receita>, IBusiness<BaseReceitaDto>
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
    public override BaseReceitaDto Create(BaseReceitaDto obj)
    {
        IsCategoriaValid(obj);
        Receita receita = _mapper.Map<Receita>(obj);
        _repositorio.Insert(ref receita);
        return _mapper.Map<BaseReceitaDto>(receita);
    }

    public override List<BaseReceitaDto> FindAll(int idUsuario)
    {
        var receitas = _repositorio.GetAll().FindAll(d => d.UsuarioId == idUsuario);
        foreach (var receita in receitas)
            receita.Categoria = _repoCategoria.Get(receita.CategoriaId);
        return _mapper.Map<List<BaseReceitaDto>>(receitas);
    }      

    public override BaseReceitaDto FindById(int id, int idUsuario)
    {
        var receita = _repositorio.Get(id);
        receita.Categoria = _repoCategoria.Get(receita.CategoriaId);
        var BaseReceitaDto = _mapper.Map<BaseReceitaDto>(receita);
        if (BaseReceitaDto.IdUsuario == idUsuario)
            return BaseReceitaDto;
        return null;
    }

    public override BaseReceitaDto Update(BaseReceitaDto obj)
    {
        IsCategoriaValid(obj);
        Receita receita = _mapper.Map<Receita>(obj);
        _repositorio.Update(ref receita);
        return _mapper.Map<BaseReceitaDto>(receita);
    }

    public override bool Delete(BaseReceitaDto obj)
    {
        Receita receita = _mapper.Map<Receita>(obj);
        return  _repositorio.Delete(receita);
    }

    private void IsCategoriaValid(BaseReceitaDto obj)
    {
        if (_repoCategoria.GetAll().Find(c => c.UsuarioId == obj.IdUsuario && c.TipoCategoria.Equals(TipoCategoria.Receita) && c.Id == obj.IdCategoria) == null)
            throw new ArgumentException("Categoria não existe cadastrada para este usuário!");
    }
}
