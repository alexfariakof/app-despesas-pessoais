using Domain.VM;

namespace Business;
public interface ILancamentoBusiness
{
    List<LancamentoVM> FindByMesAno(DateTime data, int idUsuario); 
}
