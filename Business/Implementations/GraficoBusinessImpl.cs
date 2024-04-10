using Domain.Entities;
using Repository.Persistency;

namespace Business.Implementations;
public class GraficosBusinessImpl : IGraficosBusiness
{
    private readonly IGraficosRepositorio _repositorio;

    public GraficosBusinessImpl(IGraficosRepositorio repositorio)
    {
        _repositorio = repositorio;
    }
    public Grafico GetDadosGraficoByAnoByIdUsuario(int idUsuario, DateTime data)
    {
        return _repositorio.GetDadosGraficoByAno(idUsuario, data);
    }
}
