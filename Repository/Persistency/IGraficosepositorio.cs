using Domain.Entities;

namespace Repository.Persistency;
public interface IGraficosRepositorio
{
    Grafico GetDadosGraficoByAno(int idUsuario, DateTime data);
}
