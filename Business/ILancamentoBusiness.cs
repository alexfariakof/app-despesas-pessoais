using Business.Dtos;

namespace Business;
public interface ILancamentoBusiness
{
    List<LancamentoVM> FindByMesAno(DateTime data, int idUsuario); 
}
