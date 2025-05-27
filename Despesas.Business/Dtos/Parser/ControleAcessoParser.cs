using Business.Dtos.Parser.Interfaces;
using Business.Dtos.v1;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class ControleAcessoParser : IParser<ControleAcessoDto, ControleAcesso>, IParser<ControleAcesso, ControleAcessoDto>
{

    public ControleAcessoDto Parse(ControleAcesso origin)
    {
        if (origin == null) return new();
        return new ControleAcessoDto
        {
            Id = origin.Id,
            Email = origin.Login,
            Senha = origin.Senha,
            UsuarioId = origin.UsuarioId,
            Nome = origin?.Usuario?.Nome,
            Telefone = origin?.Usuario?.Telefone,
            SobreNome = origin?.Usuario?.SobreNome,
        };
    }

    public ControleAcesso Parse(ControleAcessoDto origin)
    {
        if (origin == null) return new();
        return new ControleAcesso
        {
            Id = origin.Id,
            Login = origin?.Email ?? "",
            UsuarioId = origin.UsuarioId,
            Senha = origin?.Senha ?? "",
            Usuario = new Usuario
            {
                Id = origin.UsuarioId,
                Nome = origin?.Nome ?? "",
                SobreNome = origin?.SobreNome ?? "",
                Telefone = origin?.Telefone ?? "",
                Email = origin?.Email ?? "",
            }
        };
    }

    public List<ControleAcessoDto> ParseList(List<ControleAcesso> origin)
    {
        throw new NotImplementedException();
    }

    public List<ControleAcesso> ParseList(List<ControleAcessoDto> origin)
    {
        throw new NotImplementedException();
    }
}