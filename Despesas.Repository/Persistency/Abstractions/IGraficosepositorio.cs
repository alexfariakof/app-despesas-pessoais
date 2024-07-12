using Domain.Entities;

namespace Repository.Persistency.Abstractions;
public interface IGraficosRepositorio
{
    Grafico GetDadosGraficoByAno(Guid idUsuario, DateTime data);
}
