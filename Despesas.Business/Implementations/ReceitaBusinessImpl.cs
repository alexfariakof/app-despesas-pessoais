using AutoMapper;
using Business.Abstractions;
using Domain.Entities;
using Repository.Persistency.Generic;
using Business.Dtos.Core;
using Business.Abstractions.Generic;
using Repository.Persistency.UnitOfWork.Abstractions;
using Domain.Entities.ValueObjects;

namespace Business.Implementations;
public class ReceitaBusinessImpl<Dto> : BusinessBase<Dto, Receita>, IBusiness<Dto, Receita> where Dto : ReceitaDtoBase, new()
{
    private readonly IRepositorio<Receita> _repositorio;
    private readonly IRepositorio<Categoria> _repoCategoria;
    private readonly IMapper _mapper;
    public ReceitaBusinessImpl(IMapper mapper, IUnitOfWork<Receita> unitOfWork, IRepositorio<Receita> repositorio, IRepositorio<Categoria> repoCategoria) : base(mapper, repositorio, unitOfWork)
    {
        _mapper = mapper;
        _repositorio = repositorio;
        _repoCategoria = repoCategoria;
    }

    public override Dto Create(Dto dto)
    {
        Receita receita = _mapper.Map<Receita>(dto);
        IsValidCategoria(receita);
        _repositorio.Insert(ref receita);
        return _mapper.Map<Dto>(receita);
    }

    public override List<Dto> FindAll(Guid idUsuario)
    {
        var receitas = _repositorio.GetAll().FindAll(d => d.UsuarioId == idUsuario);
        return _mapper.Map<List<Dto>>(receitas);
    }

    public override Dto FindById(Guid id, Guid idUsuario)
    {
        var receita = _repositorio.Get(id);
        if (receita is null) return null;
        receita.UsuarioId = idUsuario;
        IsValidReceita(receita);
        var Dto = _mapper.Map<Dto>(receita);
        return Dto;
    }

    public override Dto Update(Dto dto)
    {
        Receita receita = _mapper.Map<Receita>(dto);
        IsValidReceita(receita);
        IsValidCategoria(receita);
        _repositorio.Update(ref receita);
        return _mapper.Map<Dto>(receita);
    }

    public override bool Delete(Dto dto)
    {
        Receita receita = _mapper.Map<Receita>(dto);
        IsValidReceita(receita);
        return _repositorio.Delete(receita);
    }

    private void IsValidCategoria(Receita dto)
    {
        if (_repoCategoria.GetAll().Find(c => c.UsuarioId == dto.UsuarioId && c.TipoCategoria == TipoCategoria.CategoriaType.Receita && c.Id == dto.CategoriaId) == null)
            throw new ArgumentException("Categoria inválida para este usuário!");
    }

    private void IsValidReceita(Receita dto)
    {
        if (_repositorio.Get(dto.Id)?.Usuario?.Id != dto.UsuarioId)
            throw new ArgumentException("Receita inválida para este usuário!");
    }
}
