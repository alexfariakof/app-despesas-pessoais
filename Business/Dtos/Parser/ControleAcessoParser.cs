using Business.Dtos.Parser.Interfaces;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class ControleAcessoParser : IParser<ControleAcessoDto, ControleAcesso>, IParser<ControleAcesso, ControleAcessoDto>
{  

    public ControleAcessoDto Parse(ControleAcesso origin)
    {
        if (origin == null) return null;
        return new ControleAcessoDto
        {
            Id = origin.Id,
            Email = origin.Login,
            Senha = origin.Senha,
            IdUsuario   = origin.UsuarioId,
            Nome = origin.Usuario.Nome,
            PerfilUsuario = origin.Usuario.PerfilUsuario,
            Telefone = origin.Usuario.Telefone,
            SobreNome = origin.Usuario.SobreNome,
            RefreshToken = origin.RefreshToken
        };
    }

    public ControleAcesso Parse(ControleAcessoDto origin)
    {
        if (origin == null) return null;
        return new ControleAcesso
        {
            Id = origin.Id,
            Login = origin.Email,
            UsuarioId = origin.IdUsuario,            
            Senha = origin.Senha,
            RefreshToken = origin?.RefreshToken,
            Usuario = new Usuario
            {
                Id = origin.IdUsuario,
                Nome = origin?.Nome,
                SobreNome = origin?.SobreNome,
                Telefone = origin?.Telefone,
                Email = origin?.Email,
                PerfilUsuario = origin.PerfilUsuario                
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