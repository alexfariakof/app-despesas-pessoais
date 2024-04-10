using Domain.Entities;

namespace Repository.Persistency;
public interface ILancamentoRepositorio
{
    List<Lancamento> FindByMesAno(DateTime data, int idUsuario);
}
