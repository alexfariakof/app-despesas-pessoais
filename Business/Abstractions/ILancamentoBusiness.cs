using Business.Dtos.Core;

namespace Business.Abstractions;
public interface ILancamentoBusiness
{
    List<BaseLancamentoDto> FindByMesAno(DateTime data, int idUsuario); 
}
