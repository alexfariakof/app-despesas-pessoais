using Business.Dtos;

namespace Business.Abstractions;
public interface ILancamentoBusiness
{
    List<LancamentoDto> FindByMesAno(DateTime data, int idUsuario); 
}
