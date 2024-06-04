using Domain.Entities;

namespace Repository.Persistency.Abstractions;
public interface IGraficosRepositorio
{
    Grafico GetDadosGraficoByAno(int idUsuario, DateTime data);
}
