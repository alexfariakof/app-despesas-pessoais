using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VO;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class UsuarioMap : IParser<UsuarioVO, Usuario>, IParser<Usuario, UsuarioVO>
    {

        public Usuario Parse(UsuarioVO origin)
        {
            if (origin == null) return new Usuario();
            return new Usuario
            {
                Id  = origin.Id,
                Email = origin.Email,
                Nome = origin.Nome,
                sobreNome = origin.sobreNome,
                telefone = origin.telefone                
            };
        }

        public UsuarioVO Parse(Usuario origin)
        {
            if (origin == null) return new UsuarioVO();
            return new UsuarioVO
            {
                Id = origin.Id,
                Email = origin.Email,
                Nome = origin.Nome,
                sobreNome = origin.sobreNome,
                telefone = origin.telefone
            };
        }

        public List<Usuario> ParseList(List<UsuarioVO> origin)
        {
            if (origin == null) return new List<Usuario>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<UsuarioVO> ParseList(List<Usuario> origin)
        {
            if (origin == null) return new List<UsuarioVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
