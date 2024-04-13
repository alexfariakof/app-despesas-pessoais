using Domain.Entities;

namespace Business.Abstractions;
public interface IGraficosBusiness
{
    Grafico GetDadosGraficoByAnoByIdUsuario(int idUsuario, DateTime data);
}
