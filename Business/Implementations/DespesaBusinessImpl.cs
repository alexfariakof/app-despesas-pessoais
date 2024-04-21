using Business.Dtos;
using Business.Dtos.Parser;
using Business.Generic;
using Domain.Entities;
using Repository.Persistency.Generic;

namespace Business.Implementations;
public class DespesaBusinessImpl : IBusiness<DespesaDto>
{
    private readonly IRepositorio<Despesa> _repositorio;
    private readonly IRepositorio<Categoria> _repoCategoria;
    private readonly DespesaParser _converter;
    public DespesaBusinessImpl(IRepositorio<Despesa> repositorio, IRepositorio<Categoria> repoCategoria)
    {
        _repositorio = repositorio;
        _repoCategoria = repoCategoria;
        _converter = new DespesaParser();
    }
    public DespesaDto Create(DespesaDto obj)
    {
        IsCategoriaValid(obj);
        Despesa despesa = _converter.Parse(obj);
        _repositorio.Insert(ref despesa);
        return _converter.Parse(despesa);
    }

    public List<DespesaDto> FindAll(int idUsuario)
    {
        var despesas = _repositorio.GetAll().FindAll(d => d.UsuarioId == idUsuario);
        foreach (var despesa in despesas)
            despesa.Categoria = _repoCategoria.Get(despesa.CategoriaId);
        return _converter.ParseList(despesas);
    }

    public DespesaDto FindById(int id, int idUsuario)
    {
        var despesa = _repositorio.Get(id);
        despesa.Categoria = _repoCategoria.Get(despesa.CategoriaId);
        var despesaDto = _converter.Parse(despesa);

        if (despesaDto.IdUsuario == idUsuario)
            return despesaDto;
        return null;
    }

    public DespesaDto Update(DespesaDto obj)
    {
        IsCategoriaValid(obj);
        Despesa despesa = _converter.Parse(obj);
        _repositorio.Update(ref despesa);
        return _converter.Parse(despesa);
    }

    public bool Delete(DespesaDto obj)
    {
        Despesa despesa = _converter.Parse(obj);
        return _repositorio.Delete(despesa);
    }
    private void IsCategoriaValid(DespesaDto obj)
    {
        if (_repoCategoria.GetAll().Find(c => c.UsuarioId == obj.IdUsuario && obj.Categoria.IdTipoCategoria == (int)TipoCategoria.Despesa) == null)
            throw new ArgumentException("Erro Categoria inexistente ou não cadastrada!");
    }
}
