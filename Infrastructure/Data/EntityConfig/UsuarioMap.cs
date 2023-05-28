using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class UsuarioMap : IParser<UsuarioVM, Usuario>, IParser<Usuario, UsuarioVM>
    {

        public Usuario Parse(UsuarioVM origin)
        {
            if (origin == null) return new Usuario();
            return new Usuario
            {
                Id  = origin.Id,
                Email = origin.Email,
                Nome = origin.Nome,
                SobreNome = origin.SobreNome,
                Telefone = origin.Telefone                
            };
        }

        public UsuarioVM Parse(Usuario origin)
        {
            if (origin == null) return new UsuarioVM();
            return new UsuarioVM
            {
                Id = origin.Id,
                Email = origin.Email,
                Nome = origin.Nome,
                SobreNome = origin.SobreNome,
                Telefone = origin.Telefone
            };
        }

        public List<Usuario> ParseList(List<UsuarioVM> origin)
        {
            if (origin == null) return new List<Usuario>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<UsuarioVM> ParseList(List<Usuario> origin)
        {
            if (origin == null) return new List<UsuarioVM>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
