using Domain.Entities;

namespace Repository.Persistency.Abstractions;
public interface ILancamentoRepositorio
{
    List<Lancamento> FindByMesAno(DateTime data, int idUsuario);
}
