using Domain.Entities;

namespace Business;
public interface IGraficosBusiness
{
    Grafico GetDadosGraficoByAnoByIdUsuario(int idUsuario, DateTime data);
}
